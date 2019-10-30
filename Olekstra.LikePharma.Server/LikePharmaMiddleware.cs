namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Middleware-класс для обработки запросов к API.
    /// </summary>
    public class LikePharmaMiddleware : IMiddleware
    {
        /// <summary>
        /// Имя заголовка, содержащего аутентификационный токен.
        /// </summary>
        public const string AuthorizationTokenHeaderName = "authorization-token";

        /// <summary>
        /// Имя заголовка, содержащего аутентификационный секрет.
        /// </summary>
        public const string AuthorizationSecretHeaderName = "authorization-secret";

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
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var response = context.Response;

            var headers = context.Request.Headers;
            var authToken = headers[AuthorizationTokenHeaderName].ToString();
            var authSecret = headers[AuthorizationSecretHeaderName].ToString();

            if (string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(authSecret))
            {
                logger.LogDebug($"Запрос с неполной аутентификацией ({authToken}/{authSecret}), отвечаю кодом 401");
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var service = context.RequestServices.GetRequiredService<ILikePharmaService>();

            var userId = await service.AuthorizeAsync(authToken, authSecret).ConfigureAwait(false);
            if (userId == null)
            {
                logger.LogWarning($"Запрос с некорректной аутентификацией (токен {authToken}), отвечаю кодом 403");
                response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            logger.LogInformation($"Аутентификация успешна: token {authToken} -> user {userId}");
        }
    }
}