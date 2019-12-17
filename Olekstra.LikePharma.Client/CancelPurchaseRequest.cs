namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Отмена покупки.
    /// </summary>
    [CardOrPhoneNumber]
    [XmlRoot("cancel_purchase_request")]
    public class CancelPurchaseRequest : RequestBase<CancelPurchaseResponse>
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
        /// Идентификатор аптеки. Строка, уникальная внутри аптечной сети.
        /// </summary>
        [PharmacyId]
        [JsonPropertyName("pharmacy_id")]
        [XmlElement("pharmacy_id")]
        public string? PharmacyId { get; set; }

        /// <summary>
        /// Номер карты для идентификации клиента.
        /// </summary>
        /// <remarks>Используется либо номер карты, либо номер телефона.</remarks>
        [CardNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.CardNumberInvalid))]
        [JsonPropertyName("card_number")]
        [XmlElement("card_number")]
        public string? CardNumber { get; set; }

        /// <summary>
        /// Номер телефона для идентификации клиента.
        /// </summary>
        /// <remarks>Используется либо номер карты, либо номер телефона.</remarks>
        [PhoneNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PhoneNumberInvalid))]
        [JsonPropertyName("phone_number")]
        [XmlElement("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Секретное значение.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [JsonPropertyName("trust_key")]
        [XmlElement("trust_key")]
        public string? TrustKey { get; set; }

        /// <summary>
        /// Коды транзакции из параметра <see cref="ConfirmPurchaseRequest.Transactions"/> подтверждения продажи.
        /// </summary>
        [NonEmptyCollection]
        [JsonPropertyName("transactions")]
        [XmlArray("transactions")]
        [XmlArrayItem("transaction")]
        public List<string> Transactions { get; set; } = new List<string>();
    }
}
