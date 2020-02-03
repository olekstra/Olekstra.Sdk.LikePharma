namespace Olekstra.LikePharma.Server
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Olekstra.LikePharma.Client;
    using Xunit;

    public class LikePharmaMiddlewareTests
    {
        private readonly HttpContext context;

        private readonly Mock<ILikePharmaService<SampleUserInfo>> likeServiceMock;

        private readonly LikePharmaMiddleware<SampleUserInfo> middleware;

        private readonly LikePharmaMiddlewareOptions options = new LikePharmaMiddlewareOptions();

        public LikePharmaMiddlewareTests()
        {
            context = new DefaultHttpContext();

            context.Request.Path = "/some_path";
            context.Request.Headers[Globals.AuthorizationTokenHeaderName] = "some-token";
            context.Request.Headers[Globals.AuthorizationSecretHeaderName] = "some-secret";

            var servicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            likeServiceMock = new Mock<ILikePharmaService<SampleUserInfo>>(MockBehavior.Strict);

            context.RequestServices = servicesMock.Object;
            servicesMock.Setup(x => x.GetService(typeof(ILikePharmaService<SampleUserInfo>))).Returns(likeServiceMock.Object);

            likeServiceMock
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret", context.Request))
                .ReturnsAsync(new SampleUserInfo())
                .Verifiable();

            middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, options, new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public async Task DoNothingWithoutAuthorization(bool noToken, bool noSecret)
        {
            if (noToken)
            {
                context.Request.Headers.Remove(Globals.AuthorizationTokenHeaderName);
            }

            if (noSecret)
            {
                context.Request.Headers.Remove(Globals.AuthorizationSecretHeaderName);
            }

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(401, context.Response.StatusCode);
        }

        [Fact]
        public async Task Return403OnInvalidAuth()
        {
            likeServiceMock.Reset();

            likeServiceMock
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret", context.Request))
                .ReturnsAsync(default(SampleUserInfo))
                .Verifiable();

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(403, context.Response.StatusCode);
            likeServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Return400OnInvalidJson()
        {
            var jsonBytes = Encoding.UTF8.GetBytes("{\"fieldA\":123, and some broken json here");

            using var body = new MemoryStream();
            body.Write(jsonBytes, 0, jsonBytes.Length);
            body.Position = 0;

            context.Request.Method = "POST";
            context.Request.Path = "/get_programs";
            context.Request.ContentType = "application/json";
            context.Request.Body = body;

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(400, context.Response.StatusCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ItWorks(bool useEmptyRawRequestProcessor)
        {
            var sampleProgramName = "Программа 2+2 - скидка 22%";

            var jsonText = "{\"pos_id\": \"123\"}";
            var jsonBytes = Encoding.UTF8.GetBytes(jsonText);

            using var body = new MemoryStream();
            body.Write(jsonBytes, 0, jsonBytes.Length);
            body.Position = 0;

            context.Request.Method = "POST";
            context.Request.Path = "/get_programs";
            context.Request.ContentType = "application/json";
            context.Request.Body = body;

            context.Response.Body = new MemoryStream();

            var sampleResponse = new GetProgramsResponse
            {
                Status = Globals.StatusSuccess,
                ErrorCode = 0,
                Message = nameof(ItWorks),
                Programs = { new GetProgramsResponse.Program { Code = "1", Name = sampleProgramName } },
            };

            likeServiceMock
                .Setup(x => x.GetProgramsAsync(It.Is<GetProgramsRequest>(grp => grp.PosId == "123"), It.IsNotNull<SampleUserInfo>()))
                .ReturnsAsync(sampleResponse)
                .Verifiable();

            if (useEmptyRawRequestProcessor)
            {
                options.RawRequestProcessor = (context, user) => null;
            }

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(200, context.Response.StatusCode);
            likeServiceMock.Verify();

            context.Response.Body.Position = 0;

            using var sr = new StreamReader(context.Response.Body);
            var resp = await sr.ReadToEndAsync().ConfigureAwait(false);
            Assert.Contains(options.JsonSerializerOptions.Encoder.Encode(sampleProgramName), resp, StringComparison.Ordinal);
        }

        [Fact]
        public async Task UseUrlEncodingIfNeeded()
        {
            var sampleProgramName = "Программа 2+2 - скидка 22%";

            var jsonText = "{\"pos_id\": \"123\"}";
            var jsonBytes = Encoding.UTF8.GetBytes(System.Net.WebUtility.UrlEncode(jsonText)); // отличие от предыдущего теста

            options.UseUrlEncode = true; // второе важное отличие от предыдущего теста

            using var body = new MemoryStream();
            body.Write(jsonBytes, 0, jsonBytes.Length);
            body.Position = 0;

            context.Request.Method = "POST";
            context.Request.Path = "/get_programs";
            context.Request.ContentType = "application/json";
            context.Request.Body = body;

            context.Response.Body = new MemoryStream();

            var sampleResponse = new GetProgramsResponse
            {
                Status = Globals.StatusSuccess,
                ErrorCode = 0,
                Message = nameof(ItWorks),
                Programs = { new GetProgramsResponse.Program { Code = "1", Name = sampleProgramName } },
            };

            likeServiceMock
                .Setup(x => x.GetProgramsAsync(It.Is<GetProgramsRequest>(grp => grp.PosId == "123"), It.IsNotNull<SampleUserInfo>()))
                .ReturnsAsync(sampleResponse)
                .Verifiable();

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(200, context.Response.StatusCode);
            likeServiceMock.Verify();

            context.Response.Body.Position = 0;

            using var sr = new StreamReader(context.Response.Body);
            var respText = await sr.ReadToEndAsync().ConfigureAwait(false);
            Assert.Contains(options.JsonSerializerOptions.Encoder.Encode(sampleProgramName), System.Net.WebUtility.UrlDecode(respText), StringComparison.Ordinal);
        }

        [Fact]
        public async Task RawProcessorCalled()
        {
            PathString unknownPath = "/hello_world";
            var expectedStatusCode = 777;
            var rawProcessorInvokeCount = 0;

            options.RawRequestProcessor = (context, user) =>
            {
                rawProcessorInvokeCount++;
                context.Response.StatusCode = expectedStatusCode;
                return Task.CompletedTask;
            };

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(1, rawProcessorInvokeCount);
            Assert.Equal(expectedStatusCode, context.Response.StatusCode);
        }
    }
}
