namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Middleware-класс для обработки запросов к API.
    /// </summary>
    public class LikePharmaMiddleware : IMiddleware
    {
        private readonly ILogger logger;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="logger">Экземпляр логгера.</param>
        public LikePharmaMiddleware(ILogger<LikePharmaMiddleware> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return next(context);
        }
    }
}