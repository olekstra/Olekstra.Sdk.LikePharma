namespace Olekstra.LikePharma.Server
{
    using System;
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
                context.Request.Headers[LikePharmaMiddleware<SampleUserInfo>.AuthorizationTokenHeaderName] = "some-token";
            }

            if (!noSecret)
            {
                context.Request.Headers[LikePharmaMiddleware<SampleUserInfo>.AuthorizationSecretHeaderName] = "some-secret";
            }

            var middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, Policy.CreateEmpty(), new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(401, context.Response.StatusCode);
        }

        [Fact]
        public async Task Return403OnInvalidAuth()
        {
            var context = new DefaultHttpContext();

            context.Request.Headers[LikePharmaMiddleware<SampleUserInfo>.AuthorizationTokenHeaderName] = "some-token";
            context.Request.Headers[LikePharmaMiddleware<SampleUserInfo>.AuthorizationSecretHeaderName] = "some-secret";

            var servicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var likeService = new Mock<ILikePharmaService<SampleUserInfo>>(MockBehavior.Strict);

            context.RequestServices = servicesMock.Object;
            servicesMock.Setup(x => x.GetService(typeof(ILikePharmaService<SampleUserInfo>))).Returns(likeService.Object);

            likeService
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret", context.Request))
                .ReturnsAsync(default(SampleUserInfo))
                .Verifiable();

            var middleware = new LikePharmaMiddleware<SampleUserInfo>(_ => Task.CompletedTask, Policy.CreateEmpty(), new Mock<ILogger<LikePharmaMiddleware<SampleUserInfo>>>().Object);

            await middleware.InvokeAsync(context).ConfigureAwait(false);

            Assert.Equal(403, context.Response.StatusCode);
            likeService.Verify();
        }
    }
}
