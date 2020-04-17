namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Клиент для работы по протоколу лайк-фарма.
    /// </summary>
    public class LikePharmaClient : ILikePharmaClient
    {
        private readonly LikePharmaClientOptions options;
        private readonly HttpClient httpClient;
        private readonly LikePharmaValidator validator;
        private readonly ILogger logger;

        /// <summary>
        /// Основной конструктор.
        /// </summary>
        /// <param name="options">Настройки.</param>
        /// <param name="httpClient">Готовый и сконфигурированный HTTP Client (рекомендуется использовать HttpClientFactory).</param>
        /// <param name="logger">Логгер.</param>
        public LikePharmaClient(LikePharmaClientOptions options, HttpClient httpClient, ILogger<LikePharmaClient> logger)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.validator = new LikePharmaValidator(options.ProtocolSettings);
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            return MakeRequestAsync<RegisterRequest, RegisterResponse>("register", request);
        }

        /// <inheritdoc />
        public Task<ConfirmCodeResponse> ConfirmCodeAsync(ConfirmCodeRequest request)
        {
            return MakeRequestAsync<ConfirmCodeRequest, ConfirmCodeResponse>("confirm_code", request);
        }

        /// <inheritdoc />
        public Task<GetDiscountResponse> GetDiscountAsync(GetDiscountRequest request)
        {
            return MakeRequestAsync<GetDiscountRequest, GetDiscountResponse>("get_discount", request);
        }

        /// <inheritdoc />
        public Task<ConfirmPurchaseResponse> ConfirmPurchaseAsync(ConfirmPurchaseRequest request)
        {
            return MakeRequestAsync<ConfirmPurchaseRequest, ConfirmPurchaseResponse>("confirm_purchase", request);
        }

        /// <inheritdoc />
        public Task<CancelPurchaseResponse> CancelPurchaseAsync(CancelPurchaseRequest request)
        {
            return MakeRequestAsync<CancelPurchaseRequest, CancelPurchaseResponse>("cancel_purchase", request);
        }

        /// <inheritdoc />
        public Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request)
        {
            return MakeRequestAsync<GetProductsRequest, GetProductsResponse>("get_products", request);
        }

        /// <inheritdoc />
        public Task<GetProgramsResponse> GetProgramsAsync(GetProgramsRequest request)
        {
            return MakeRequestAsync<GetProgramsRequest, GetProgramsResponse>("get_programs", request);
        }

        private async Task<TResponse> MakeRequestAsync<TRequest, TResponse>(string path, TRequest request)
            where TRequest : RequestBase<TResponse>
            where TResponse : ResponseBase
        {
            if (options.ValidateRequests)
            {
                if (!validator.TryValidateObject(request, out var results))
                {
                    logger.LogError("Invalid request (validation failed): " + JsonSerializer.Serialize(request, options.JsonSerializerOptions));
                    var ex = new ArgumentException(ValidationMessages.RequestValidationFailed, nameof(request));
                    ex.Data["ValidationErrors"] = results;
                    throw ex;
                }
            }

            using var reqContent = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(request, options.JsonSerializerOptions));
            reqContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, path);
            httpReq.Headers.Add(Globals.AuthorizationTokenHeaderName, options.AuthorizationToken);
            httpReq.Headers.Add(Globals.AuthorizationSecretHeaderName, options.AuthorizationSecret);
            httpReq.Content = reqContent;

            using var httpResp = await httpClient.SendAsync(httpReq).ConfigureAwait(false);
            var respText = await httpResp.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!httpResp.IsSuccessStatusCode)
            {
                logger.LogDebug($"Non-successful status code received: {httpResp.StatusCode}, with body:\r\n{respText}");
                httpResp.EnsureSuccessStatusCode(); // will raise correct exception
            }

            var resp = JsonSerializer.Deserialize<TResponse>(respText, options.JsonSerializerOptions);
            if (options.ValidateResponses)
            {
                if (!validator.TryValidateObject(request, out var results))
                {
                    logger.LogError("Invalid response (validation failed): " + respText);
                    var ex = new ApplicationException(ValidationMessages.ResponseValidationFailed);
                    ex.Data["ValidationErrors"] = results;
                    throw ex;
                }
            }

            return resp;
        }
    }
}
