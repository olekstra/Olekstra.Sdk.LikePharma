﻿namespace Olekstra.LikePharma.Server.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Olekstra.LikePharma.Client;

    // <inheritdocs />
    public class SampleLikePharmaService : ILikePharmaService<string>
    {
        private readonly ILogger logger;

        public SampleLikePharmaService(ILogger<SampleLikePharmaService> logger)
        {
            this.logger = logger;
        }

        // <inheritdocs />
        public Task<string?> AuthorizeAsync(string authorizationToken, string authorizationSecret, HttpRequest httpRequest)
        {
            if (string.IsNullOrEmpty(authorizationToken) || string.IsNullOrEmpty(authorizationSecret))
            {
                return Task.FromResult<string?>(null);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase - как хочу так и проверяю пароль
            var validSecret = authorizationSecret.ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (string.Equals(authorizationSecret, validSecret, StringComparison.Ordinal))
            {
                logger.LogInformation("Успешная авторизация: " + authorizationToken);
                return Task.FromResult<string?>(authorizationToken.ToUpperInvariant());
            }
            else
            {
                logger.LogWarning("Неуспешная авторизация: " + authorizationToken);
                return Task.FromResult<string?>(null);
            }
        }

        // <inheritdocs />
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<ConfirmCodeResponse> ConfirmCodeAsync(ConfirmCodeRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<GetDiscountResponse> GetDiscountAsync(GetDiscountRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<ConfirmPurchaseResponse> ConfirmPurchaseAsync(ConfirmPurchaseRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<CancelPurchaseResponse> CancelPurchaseAsync(CancelPurchaseRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request, string user)
        {
            throw new NotImplementedException();
        }

        // <inheritdocs />
        public Task<GetProgramsResponse> GetProgramsAsync(GetProgramsRequest request, string user)
        {
            var resp = new GetProgramsResponse
            {
                Status = Globals.StatusSuccess,
                ErrorCode = 0,
                Message = "Данные сформированы",
                Programs = new List<GetProgramsResponse.Program>
                {
                    new GetProgramsResponse.Program
                    {
                         Code = "SAMPLE1",
                         Name = "Программа 1",
                    },
                    new GetProgramsResponse.Program
                    {
                         Code = "SAMPLE2",
                         Name = "Программа 2",
                    },
                },
            };

            return Task.FromResult(resp);
        }
    }
}
