namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Text.Json;
    using Olekstra.LikePharma.Client;

    /// <summary>
    /// Параметры работы протокола.
    /// </summary>
    public class LikePharmaMiddlewareOptions
    {
        /// <summary>
        /// Политика валидации данных (по умолчанию <see cref="Policy.CreateAstraZenecaPolicy()"/>).
        /// </summary>
        public Policy Policy { get; set; } = Policy.CreateAstraZenecaPolicy();

        /// <summary>
        /// Параметры JSON-сериализации.
        /// </summary>
        /// <remarks>
        /// По умолчанию содержит <see cref="LikePharmaClientOptions.CreateDefaultJsonSerializerOptions"/>.
        /// </remarks>
        public JsonSerializerOptions? JsonSerializerOptions { get; set; } = LikePharmaClientOptions.CreateDefaultJsonSerializerOptions();

        /// <summary>
        /// Устанавливает свойство <see cref="Policy"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        /// <exception cref="ArgumentNullException">Если передано пустое (null) значение.</exception>
        public LikePharmaMiddlewareOptions WithPolicy(Policy value)
        {
            this.Policy = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="JsonSerializerOptions"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение (или null для использования настроек по умолчанию).</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public LikePharmaMiddlewareOptions WithJsonOptions(JsonSerializerOptions? value)
        {
            this.JsonSerializerOptions = value;
            return this;
        }
    }
}
