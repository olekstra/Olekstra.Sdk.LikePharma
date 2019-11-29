namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Базовый класс для всех ответов (содержит поля <see cref="Status"/>, <see cref="ErrorCode"/>, <see cref="Message"/>).
    /// </summary>
    [ErrorCodeMatchStatus]
    public abstract class ResponseBase
    {
        /// <summary>
        /// Результат операции: <c>success</c> (успех) или <c>error</c> (ошибка).
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [Status(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.StatusInvalid))]
        [JsonPropertyName("status")]
        [XmlElement("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Код ошибки (<c>0</c> елси ошибок не было).
        /// </summary>
        [Range(0, 9999, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.StatusInvalid))]
        [JsonPropertyName("error_code")]
        [XmlElement("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Ответ системы (для пользователя).
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [JsonPropertyName("message")]
        [XmlElement("message")]
        public string? Message { get; set; }
    }
}
