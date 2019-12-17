namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Подтверждение покупки.
    /// </summary>
    [CardOrPhoneNumber]
    [XmlRoot("confirm_purchase_request")]
    public class ConfirmPurchaseRequest : RequestBase<ConfirmPurchaseResponse>
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
        /// Коды транзакции из параметра <see cref="GetDiscountResponse.Order.Transaction"/> из запроса на расчёт скидки.
        /// </summary>
        [NonEmptyCollection]
        [JsonPropertyName("transactions")]
        [XmlArray("transactions")]
        [XmlArrayItem("transaction")]
        public List<string> Transactions { get; set; } = new List<string>();

        /// <summary>
        /// Параметры для участия в программе отправки данных о продажах в реальном времени.
        /// </summary>
        [EmptyCollectionWithoutEmptyElements]
        [JsonPropertyName("skus")]
        [XmlArray("skus")]
        [XmlArrayItem("sku")]
        public List<Sku>? Skus { get; set; }

        /// <summary>
        /// Параметры для участия в программе отправки данных о продажах в реальном времени.
        /// </summary>
        public class Sku
        {
            /// <summary>
            /// Штрихкод (заводской штрихкод) продукта.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("barcode")]
            [XmlElement("barcode")]
            public string? Barcode { get; set; }

            /// <summary>
            /// Количество продукта со таким штрихкодом в чеке.
            /// </summary>
            [PositiveDecimal]
            [JsonPropertyName("count")]
            [XmlElement("count")]
            public decimal Count { get; set; }

            /// <summary>
            /// Цена за единицу продукта.
            /// </summary>
            [PositiveDecimal]
            [JsonPropertyName("price")]
            [XmlElement("price")]
            public decimal Price { get; set; }
        }
    }
}
