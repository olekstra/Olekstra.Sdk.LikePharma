namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Ответ на запрос <see cref="GetDiscountRequest"/>.
    /// </summary>
    [CardOrPhoneNumber]
    [XmlRoot("get_discount_response")]
    public class GetDiscountResponse : ResponseBase
    {
        /// <summary>
        /// Идентификатор кассового терминала.
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
        /// Номер карты, который был отправлен вместе с запросом.
        /// </summary>
        /// <remarks>Используется либо номер карты, либо номер телефона.</remarks>
        [CardNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.CardNumberInvalid))]
        [JsonPropertyName("card_number")]
        [XmlElement("card_number")]
        public string? CardNumber { get; set; }

        /// <summary>
        /// Номер телефона, который был отправлен вместе с запросом.
        /// </summary>
        /// <remarks>Используется либо номер карты, либо номер телефона.</remarks>
        [PhoneNumber(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PhoneNumberInvalid))]
        [JsonPropertyName("phone_number")]
        [XmlElement("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Данные, которые были отправлены вместе с запросом.
        /// </summary>
        [JsonPropertyName("any_data")]
        [XmlElement("any_data")]
        public string? AnyData { get; set; }

        /// <summary>
        /// Отдельное сообщение для фармацевта об условиях снижения цены.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Список товарных позиций.
        /// </summary>
        [NonEmptyCollectiocInSuccessulResponse]
        [JsonPropertyName("orders")]
        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Описание товарной позиции при расчёте скидки.
        /// </summary>
        [ErrorCodeMatchDiscount]
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
            [PositiveDecimal]
            [JsonPropertyName("count")]
            [XmlElement("count")]
            public decimal Count { get; set; }

            /// <summary>
            /// Наименование препарата (если ШК распознан).
            /// </summary>
            [JsonPropertyName("description")]
            [XmlElement("description")]
            public string? Description { get; set; }

            /// <summary>
            /// Размер снижения цены (в рублях), "для информации".
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [NonNegativeDecimal]
            [JsonPropertyName("discount")]
            [XmlElement("discount")]
            public decimal Discount { get; set; }

            /// <summary>
            /// Код ошибки расчёта снижения цены по данной позиции (<c>0</c> елси ошибок не было).
            /// </summary>
            [Range(0, 9999, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.StatusInvalid))]
            [JsonPropertyName("error_code")]
            [XmlElement("error_code")]
            public int ErrorCode { get; set; }

            /// <summary>
            /// Текст результат расчёта снижения цены по данной позиции.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("message")]
            [XmlElement("message")]
            public string? Message { get; set; }

            /// <summary>
            /// Код транзакции, передаётся на сервер для подтверждения покупки данной позиции.
            /// </summary>
            [JsonPropertyName("transaction")]
            [XmlElement("transaction")]
            public string? Transaction { get; set; }

            /// <summary>
            /// Тип снижения цены: рубли (<c>cash</c>) или проценты (<c>percent</c>).
            /// </summary>
            [OrderDiscountType]
            [JsonPropertyName("type")]
            [XmlElement("type")]
            public string? Type { get; set; }

            /// <summary>
            /// Итоговая сумма с учетом снижения цены.
            /// </summary>
            [NonNegativeDecimal]
            [JsonPropertyName("value")]
            [XmlElement("value")]
            public decimal Value { get; set; }

            /// <summary>
            /// Итоговая сумма <see cref="Value"/>, разделенная на количество продукта в позиции в чеке (<see cref="Count"/>).
            /// </summary>
            [NonNegativeDecimal]
            [JsonPropertyName("value_per_item")]
            [XmlElement("value_per_item")]
            public decimal ValuePerItem { get; set; }

            /// <summary>
            /// Название дисконтной программы, по которой был выполнен расчёт снижения цены.
            /// </summary>
            [JsonPropertyName("program")]
            [XmlElement("program")]
            public string? Program { get; set; }
        }
    }
}
