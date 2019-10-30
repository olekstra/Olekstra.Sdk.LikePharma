namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
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
                context.Request.Headers[LikePharmaMiddleware.AuthorizationTokenHeaderName] = "some-token";
            }

            if (!noSecret)
            {
                context.Request.Headers[LikePharmaMiddleware.AuthorizationSecretHeaderName] = "some-secret";
            }

            var middleware = new LikePharmaMiddleware(new Mock<ILogger<LikePharmaMiddleware>>().Object);

            await middleware.InvokeAsync(context, _ => Task.CompletedTask).ConfigureAwait(false);

            Assert.Equal(401, context.Response.StatusCode);
        }

        [Fact]
        public async Task Return403OnInvalidAuth()
        {
            var context = new DefaultHttpContext();

            context.Request.Headers[LikePharmaMiddleware.AuthorizationTokenHeaderName] = "some-token";
            context.Request.Headers[LikePharmaMiddleware.AuthorizationSecretHeaderName] = "some-secret";

            var servicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var likeService = new Mock<ILikePharmaService>(MockBehavior.Strict);

            context.RequestServices = servicesMock.Object;
            servicesMock.Setup(x => x.GetService(typeof(ILikePharmaService))).Returns(likeService.Object);

            likeService
                .Setup(x => x.AuthorizeAsync("some-token", "some-secret"))
                .ReturnsAsync(default(string))
                .Verifiable();

            var middleware = new LikePharmaMiddleware(new Mock<ILogger<LikePharmaMiddleware>>().Object);

            await middleware.InvokeAsync(context, _ => Task.CompletedTask).ConfigureAwait(false);

            Assert.Equal(403, context.Response.StatusCode);
            likeService.Verify();
        }
    }
}
