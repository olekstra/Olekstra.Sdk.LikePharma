namespace Olekstra.LikePharma.Server.Demo
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    // <inheritdocs />
    public class SampleLikePharmaService : ILikePharmaService
    {
        private readonly ILogger logger;

        public SampleLikePharmaService(ILogger<SampleLikePharmaService> logger)
        {
            this.logger = logger;
        }

        // <inheritdocs />
        public Task<string?> AuthorizeAsync(string authorizationToken, string authorizationSecret)
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
    }
}
