namespace Olekstra.LikePharma.Client
{
    using System;
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

        /// <summary>
        /// Копирует в данный объект поля из предоставленного объекта.
        /// </summary>
        /// <param name="source">Исходный объект, поля которого надо скопировать.</param>
        /// <exception cref="ArgumentNullException">Если в параметре 'source' передано значение <b>null</b>.</exception>
        protected void CopyFrom(ResponseBase source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));

            this.Status = source.Status;
            this.ErrorCode = source.ErrorCode;
            this.Message = source.Message;
        }

        /// <summary>
        /// Копирует в предоставленный объект поля из данного объекта.
        /// </summary>
        /// <param name="destination">Целевой объект, в который надо скопировать значения полей.</param>
        /// <exception cref="ArgumentNullException">Если в параметре 'destination' передано значение <b>null</b>.</exception>
        protected void CopyTo(ResponseBase destination)
        {
            destination = destination ?? throw new ArgumentNullException(nameof(destination));

            destination.Status = this.Status;
            destination.ErrorCode = this.ErrorCode;
            destination.Message = this.Message;
        }
    }
}
