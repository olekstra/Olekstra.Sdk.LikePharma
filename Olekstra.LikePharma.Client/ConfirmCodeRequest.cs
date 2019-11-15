namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Подтверждение номера телефона по коду из СМС.
    /// </summary>
    [XmlRoot("confirm_code_request")]
    public class ConfirmCodeRequest
    {
        /// <summary>
        /// Идентификатор кассового терминала. Любая строка длиной до 40 символов, уникальная внутри аптечной сети.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [PosId(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PosIdInvalid))]
        [JsonPropertyName("pos_id")]
        [XmlElement("pos_id")]
        public string? PosId { get; set; }

        /// <summary>
        /// Код из СМС (цифры без разделителей).
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [MinLength(1, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.MinLengthFailed))]
        [JsonPropertyName("code")]
        [XmlElement("code")]
        public string? Code { get; set; }
    }
}
