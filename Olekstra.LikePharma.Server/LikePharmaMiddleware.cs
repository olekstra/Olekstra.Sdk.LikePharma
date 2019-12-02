﻿namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Olekstra.LikePharma.Client;

    /// <summary>
    /// Middleware-класс для обработки запросов к API.
    /// </summary>
    /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
    public class LikePharmaMiddleware<TUser>
        where TUser : class
    {
        /// <summary>
        /// Имя заголовка, содержащего аутентификационный токен.
        /// </summary>
        public const string AuthorizationTokenHeaderName = "authorization-token";

        /// <summary>
        /// Имя заголовка, содержащего аутентификационный секрет.
        /// </summary>
        public const string AuthorizationSecretHeaderName = "authorization-secret";

        private const string ContentTypeJson = "application/json";

        private const string ContentTypeText = "text/plain";

        private const string ContentTypeTextUtf8 = ContentTypeText + ";charset=utf-8";

        private readonly LikePharmaValidator validator;

        private readonly ILogger logger;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="next">Следующий <see cref="RequestDelegate"/> в цепочке (будет проигнорирован!).</param>
        /// <param name="policy">Политика валидации.</param>
        /// <param name="logger">Экземпляр логгера.</param>
        public LikePharmaMiddleware(RequestDelegate next, Policy policy, ILogger<LikePharmaMiddleware<TUser>> logger)
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

            if (request.Path == PathString.Empty && request.Method == "GET")
            {
                response.ContentType = ContentTypeTextUtf8;
                await response.WriteAsync(Messages.RootPathGetResponseText).ConfigureAwait(false);
                return;
            }

            var headers = request.Headers;
            var authToken = headers[AuthorizationTokenHeaderName].ToString();
            var authSecret = headers[AuthorizationSecretHeaderName].ToString();

            if (string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(authSecret))
            {
                logger.LogDebug($"Запрос с неполной аутентификацией ({authToken}/{authSecret}), отвечаю кодом 401");
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.ContentType = ContentTypeTextUtf8;
                await response.WriteAsync(Messages.Status401Unauthorized_ResponseText).ConfigureAwait(false);
                return;
            }

            var service = context.RequestServices.GetRequiredService<ILikePharmaService<TUser>>();

            var user = await service.AuthorizeAsync(authToken, authSecret, request).ConfigureAwait(false);
            if (user == null)
            {
                logger.LogWarning($"Запрос с некорректной аутентификацией (токен {authToken}), отвечаю кодом 403");
                response.StatusCode = StatusCodes.Status403Forbidden;
                response.ContentType = ContentTypeTextUtf8;
                await response.WriteAsync(Messages.Status403Forbidden_ResponseText).ConfigureAwait(false);
                return;
            }

            if (!string.Equals(request.Method, "POST", StringComparison.Ordinal))
            {
                logger.LogWarning($"Запрос с некорректным методом ({request.Method}), отвечаю кодом 405");
                response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                response.ContentType = ContentTypeTextUtf8;
                await response.WriteAsync(Messages.Status405MethodNotAllowed_ResponseText).ConfigureAwait(false);
                return;
            }

            logger.LogDebug($"Аутентификация успешна: token {authToken} -> {user}");

            switch (request.Path)
            {
                case "/register":
                    await Process<RegisterRequest, RegisterResponse>(service.RegisterAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/confirm_code":
                    await Process<ConfirmCodeRequest, ConfirmCodeResponse>(service.ConfirmCodeAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/get_discount":
                    await Process<GetDiscountRequest, GetDiscountResponse>(service.GetDiscountAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/confirm_purchase":
                    await Process<ConfirmPurchaseRequest, ConfirmPurchaseResponse>(service.ConfirmPurchanseAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/cancel_purchase":
                    await Process<CancelPurchaseRequest, CancelPurchaseResponse>(service.CancelPurchanseAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/get_products":
                    await Process<GetProductsRequest, GetProductsResponse>(service.GetProductsAsync, request, response, user).ConfigureAwait(false);
                    break;

                case "/get_programs":
                    await Process<GetProgramsRequest, GetProgramsResponse>(service.GetProgramsAsync, request, response, user).ConfigureAwait(false);
                    break;

                default:
                    logger.LogWarning("Неизвестный запрос, возвращаю 404: " + request.Path);
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ContentType = ContentTypeTextUtf8;
                    await response.WriteAsync(Messages.Status404NotFound_ResponseText).ConfigureAwait(false);
                    break;
            }
        }

        /// <summary>
        /// Обработчик запроса: считывает и десериализует запрос, валидирует его, вызывает обработчик, вылидирует ответ, отправляет ответ.
        /// </summary>
        /// <typeparam name="TRequest">Тип запроса.</typeparam>
        /// <typeparam name="TResponse">Тип ответа.</typeparam>
        /// <param name="processor">Обработчик.</param>
        /// <param name="httpRequest">HTTP request (to read request from).</param>
        /// <param name="httpResponse">HTTP response (to write response to).</param>
        /// <param name="user">Current (authorized) user.</param>
        /// <returns>Awaitable Task.</returns>
        public async Task Process<TRequest, TResponse>(Func<TRequest, TUser, Task<TResponse>> processor, HttpRequest httpRequest, HttpResponse httpResponse, TUser user)
            where TRequest : RequestBase<TResponse>
            where TResponse : ResponseBase, new()
        {
            processor = processor ?? throw new ArgumentNullException(nameof(processor));
            httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
            httpResponse = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
            user = user ?? throw new ArgumentNullException(nameof(user));

            TResponse resp;

            var req = await JsonSerializer.DeserializeAsync<TRequest>(httpRequest.Body);
            if (!validator.TryValidateObject(req, out var results))
            {
                resp = new TResponse
                {
                    Status = Globals.StatusError,
                    ErrorCode = StatusCodes.Status400BadRequest,
                    Message = results.First().ErrorMessage,
                };

                httpResponse.ContentType = ContentTypeJson;
                await JsonSerializer.SerializeAsync(httpResponse.Body, resp).ConfigureAwait(false);
                return;
            }

            resp = await processor(req, user).ConfigureAwait(false);
            if (!validator.TryValidateObject(resp, out results))
            {
                logger.LogDebug(JsonSerializer.Serialize(resp));
                logger.LogWarning("Errors: " + string.Join(Environment.NewLine, results.Select(x => x.MemberNames.FirstOrDefault() + " " + x.ErrorMessage)));
                throw new ApplicationException(Messages.PreparedResponseIsInvalid);
            }

            httpResponse.ContentType = ContentTypeJson;
            await JsonSerializer.SerializeAsync(httpResponse.Body, resp).ConfigureAwait(false);
        }
    }
}