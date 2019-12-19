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
        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public async Task DoNothingWithoutAuthorization(bool noToken, bool noSecret)
        {
            var context = new DefaultHttpContext();
            if (!noToken)
            {
                context.Request.Headers[Globals.AuthorizationTokenHeaderName] = "some-token";
            }

            if (!noSecret)
            {
                context.Request.Headers[Globals.AuthorizationSecretHeaderName] = "some-secret";
            }

            var middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, new LikePharmaMiddlewareOptions(),  new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(401, context.Response.StatusCode);
        }

        [Fact]
        public async Task Return403OnInvalidAuth()
        {
            var context = new DefaultHttpContext();

            context.Request.Headers[Globals.AuthorizationTokenHeaderName] = "some-token";
            context.Request.Headers[Globals.AuthorizationSecretHeaderName] = "some-secret";

            var servicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var likeService = new Mock<ILikePharmaService<SampleUserInfo>>(MockBehavior.Strict);

            context.RequestServices = servicesMock.Object;
            servicesMock.Setup(x => x.GetService(typeof(ILikePharmaService<SampleUserInfo>))).Returns(likeService.Object);

            likeService
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret", context.Request))
                .ReturnsAsync(default(SampleUserInfo))
                .Verifiable();

            var middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, new LikePharmaMiddlewareOptions(), new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(403, context.Response.StatusCode);
            likeService.Verify();
        }

        [Fact]
        public async Task Return400OnInvalidJson()
        {
            var jsonBytes = Encoding.UTF8.GetBytes("{\"fieldA\":123, and some broken json here");

            using var body = new MemoryStream();
            body.Write(jsonBytes, 0, jsonBytes.Length);
            body.Position = 0;

            var context = new DefaultHttpContext();

            context.Request.Method = "POST";
            context.Request.Path = "/get_programs";
            context.Request.Headers[Globals.AuthorizationTokenHeaderName] = "some-token";
            context.Request.Headers[Globals.AuthorizationSecretHeaderName] = "some-secret";
            context.Request.ContentType = "application/json";
            context.Request.Body = body;

            var servicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var likeService = new Mock<ILikePharmaService<SampleUserInfo>>(MockBehavior.Strict);

            context.RequestServices = servicesMock.Object;
            servicesMock.Setup(x => x.GetService(typeof(ILikePharmaService<SampleUserInfo>))).Returns(likeService.Object);

            likeService
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret", context.Request))
                .ReturnsAsync(new SampleUserInfo());

            var middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, new LikePharmaMiddlewareOptions(), new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(400, context.Response.StatusCode);
            likeService.Verify();
        }
    }
}
