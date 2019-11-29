namespace Olekstra.LikePharma.Client
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Запрос на расчёт снижения цены.
    /// </summary>
    [CardOrPhoneNumber]
    [XmlRoot("get_discount_request")]
    public class GetDiscountRequest : RequestBase<GetDiscountResponse>
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
        /// Любые данные, которые отправятся вместе с запросом. АПИ вернёт их же в ответе.
        /// </summary>
        [JsonPropertyName("any_data")]
        [XmlElement("any_data")]
        public string? AnyData { get; set; }

        /// <summary>
        /// Список товарных позиций.
        /// </summary>
        [NonEmptyCollection]
        [JsonPropertyName("orders")]
        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Описание товарной позиции при расчёте скидки.
        /// </summary>
        public class Order
        {
            /// <summary>
            /// Любые данные, которые отправятся вместе с запросом. АПИ вернёт их же в ответе.
            /// </summary>
            [JsonPropertyName("any_data")]
            [XmlElement("any_data")]
            public string? AnyData { get; set; }

            /// <summary>
            /// Штрихкод (заводской штрихкод) позиции в заказе.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("barcode")]
            [XmlElement("barcode")]
            public string? Barcode { get; set; }

            /// <summary>
            /// Количество по позиции в заказе.
            /// </summary>
            [PositiveInteger]
            [JsonPropertyName("count")]
            [XmlElement("count")]
            public int Count { get; set; }

            /// <summary>
            /// Цена за единицу в заказе. Число с плавающей точкой.
            /// </summary>
            [NonNegativeDecimal]
            [JsonPropertyName("price")]
            [XmlElement("price")]
            public decimal? Price { get; set; }
        }
    }
}
