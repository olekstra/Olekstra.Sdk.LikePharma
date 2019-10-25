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
        [Fact]
        public async Task CallsNext()
        {
            var called = 0;

            var context = new DefaultHttpContext();

            var middleware = new LikePharmaMiddleware(new Mock<ILogger<LikePharmaMiddleware>>().Object);

            await middleware.InvokeAsync(
                context,
                (HttpContext context) =>
                {
                    called++;
                    return Task.CompletedTask;
                }).ConfigureAwait(false);

            Assert.Equal(1, called);
        }
    }
}
