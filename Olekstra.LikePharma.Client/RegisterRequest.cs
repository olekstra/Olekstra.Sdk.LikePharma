namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Выдача новой карты пациенту и привязка к ней номера телефона. Привязка существующей карты пациента к номеру телефона.
    /// </summary>
    [XmlRoot("register_request")]
    public class RegisterRequest : RequestBase<RegisterResponse>
    {
        /// <summary>
        /// Идентификатор кассового терминала. Любая строка длиной до 40 символов, уникальная внутри аптечной сети.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [PosId(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PosIdInvalid))]
        [JsonPropertyName("pos_id")]
        [XmlElement("pos_id")]
        public string? PosId { get; set; }

        /// <summary>
        /// Номер выдаваемой карты.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [CardNumber]
        [JsonPropertyName("card_number")]
        [XmlElement("card_number")]
        public string? CardNumber { get; set; }

        /// <summary>
        /// Российский мобильный номер телефона.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [PhoneNumber]
        [JsonPropertyName("phone_number")]
        [XmlElement("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Ключ доверия.
        /// Необязательный параметр. Выдается оператором. Если указан - сервер не проверяет номер телефона, а сразу отдаёт код подтверждения, для передачи его в метод confirm_code.
        /// </summary>
        [JsonPropertyName("trust_key")]
        [XmlElement("trust_key")]
        public string? TrustKey { get; set; }
    }
}
