namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Unicode;

    /// <summary>
    /// Настройки для <see cref="LikePharmaClient"/>.
    /// </summary>
    public class LikePharmaClientOptions
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="authorizationToken">Аутентификационный токен.</param>
        /// <param name="authorizationSecret">Аутентификационный секрет.</param>
        public LikePharmaClientOptions(string authorizationToken, string authorizationSecret)
        {
            if (string.IsNullOrWhiteSpace(authorizationToken))
            {
                throw new ArgumentNullException(nameof(authorizationToken));
            }

            if (string.IsNullOrWhiteSpace(authorizationSecret))
            {
                throw new ArgumentNullException(nameof(authorizationSecret));
            }

            this.AuthorizationToken = authorizationToken;
            this.AuthorizationSecret = authorizationSecret;
            this.JsonSerializerOptions = CreateDefaultJsonSerializerOptions();
        }

        /// <summary>
        /// Аутентификационный токен.
        /// </summary>
        public string AuthorizationToken { get; set; }

        /// <summary>
        /// Аутентификационный секрет.
        /// </summary>
        public string AuthorizationSecret { get; set; }

        /// <summary>
        /// Набор правил (политика) валидации структур данных.
        /// </summary>
        public Policy Policy { get; set; } = Policy.CreateOlekstraPolicy();

        /// <summary>
        /// Нужно ли автоматически проверять все запросы.
        /// </summary>
        public bool ValidateRequests { get; set; } = true;

        /// <summary>
        /// Нужно ли автоматически проверять все ответы.
        /// </summary>
        public bool ValidateResponses { get; set; } = true;

        /// <summary>
        /// Настройки JSON-сериализации.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        /// <summary>
        /// Создает настройки по умолчанию для JSON-сериализатора.
        /// </summary>
        /// <remarks>
        /// В объект вносятся следующие изменения:
        /// * <see cref="JsonSerializerOptions.Encoder"/> устанавливается в JavaScriptEncoder.Create(UnicodeRanges.All),
        /// * <see cref="JsonSerializerOptions.IgnoreNullValues"/> устанавливается в <b>true</b>.
        /// </remarks>
        /// <returns>Настройки.</returns>
        public static JsonSerializerOptions CreateDefaultJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                IgnoreNullValues = true,
            };
        }
    }
}
