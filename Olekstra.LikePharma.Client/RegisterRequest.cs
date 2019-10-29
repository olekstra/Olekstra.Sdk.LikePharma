namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Validation;

    /// <summary>
    /// Выдача новой карты пациенту и привязка к ней номера телефона. Привязка существующей карты пациента к номеру телефона.
    /// </summary>
    [XmlRoot("register_request")]
    public class RegisterRequest
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
        /// Номер выдаваемой карты. 19 цифр без разделителей.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [CardNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.CardNumberInvalid))]
        [JsonPropertyName("card_number")]
        [XmlElement("card_number")]
        public string? CardNumber { get; set; }

        /// <summary>
        /// Российский мобильный номер телефона. +7 и 10 цифр без разделителей.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [PhoneNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PhoneNumberInvalid))]
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
