namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Olekstra.LikePharma.Client;

    /// <summary>
    /// Параметры работы протокола.
    /// </summary>
    public class LikePharmaMiddlewareOptions
    {
        /// <summary>
        /// Политика валидации данных (по умолчанию <see cref="ProtocolSettings.CreateAstraZeneca()"/>).
        /// </summary>
        public ProtocolSettings ProtocolSettings { get; set; } = ProtocolSettings.CreateAstraZeneca();

        /// <summary>
        /// Параметры JSON-сериализации.
        /// </summary>
        /// <remarks>
        /// По умолчанию содержит <see cref="LikePharmaClientOptions.CreateDefaultJsonSerializerOptions"/>.
        /// </remarks>
        public JsonSerializerOptions? JsonSerializerOptions { get; set; } = LikePharmaClientOptions.CreateDefaultJsonSerializerOptions();

        /// <summary>
        /// Надо ли делать URL-encode (и URL-decode) при чтении/записи JSON (по умолчанию false).
        /// </summary>
        public bool UseUrlEncode { get; set; } = false;

        /// <summary>
        /// Единый "универсальный" обработчик запросов, вызывается после авторизации польозвателя но до реальной обработки.
        /// </summary>
        /// <remarks>
        /// Если обработчик вернул Task - считается что он обработал запрос, дальнейшая обработка не производится.
        /// Если вернул null вместо Task - считается что он запрос не обработал, выполняется "обычная" обработка.
        /// Вторым параметром (object) в обработчик передается TUser (авторизованный пользователь).
        /// </remarks>
        public Func<HttpContext, object, Task?>? RawRequestProcessor { get; set; } = null;

        /// <summary>
        /// Устанавливает свойство <see cref="ProtocolSettings"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        /// <exception cref="ArgumentNullException">Если передано пустое (null) значение.</exception>
        public LikePharmaMiddlewareOptions WithProtocolSettings(ProtocolSettings value)
        {
            this.ProtocolSettings = value ?? throw new ArgumentNullException(nameof(value));
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

        /// <summary>
        /// Устанавливает свойство <see cref="UseUrlEncode"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение (или null для использования настроек по умолчанию).</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public LikePharmaMiddlewareOptions WithUrlEncode(bool value)
        {
            this.UseUrlEncode = value;
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="RawRequestProcessor"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение (или null для использования настроек по умолчанию).</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public LikePharmaMiddlewareOptions WithRawProcessor(Func<HttpContext, object, Task?>? value)
        {
            this.RawRequestProcessor = value;
            return this;
        }
    }
}
