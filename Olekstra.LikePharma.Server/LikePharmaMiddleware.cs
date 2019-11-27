namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Olekstra.LikePharma.Client;

    /// <summary>
    /// Middleware-класс для обработки запросов к API.
    /// </summary>
    public class LikePharmaMiddleware
    {
        /// <summary>
        /// Имя заголовка, содержащего аутентификационный токен.
        /// </summary>
        public const string AuthorizationTokenHeaderName = "authorization-token";

        /// <summary>
        /// Имя заголовка, содержащего аутентификационный секрет.
        /// </summary>
        public const string AuthorizationSecretHeaderName = "authorization-secret";

        private readonly LikePharmaValidator validator;

        private readonly ILogger logger;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="next">Следующий <see cref="RequestDelegate"/> в цепочке (будет проигнорирован!).</param>
        /// <param name="policy">Политика валидации.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public LikePharmaMiddleware(RequestDelegate next, Policy policy, ILogger<LikePharmaMiddleware> logger)
        {
            this.validator = new LikePharmaValidator(policy ?? throw new ArgumentNullException(nameof(policy)));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Обработчик входящих запросов.
        /// </summary>
        /// <param name="context">Экземпляр <see cref="HttpContext"/>.</param>
        /// <returns>Экземпляр Task для ожидания.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.Request;
            var response = context.Response;

            var headers = request.Headers;
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

            logger.LogDebug($"Аутентификация успешна: token {authToken} -> user {userId}");

            switch (request.Path)
            {
                ////case "/register":
                ////    break;

                default:
                    logger.LogWarning("Неизвестный запрос, возвращаю 404: " + request.Path);
                    response.StatusCode = StatusCodes.Status404NotFound;
                    break;
            }
        }
    }
}