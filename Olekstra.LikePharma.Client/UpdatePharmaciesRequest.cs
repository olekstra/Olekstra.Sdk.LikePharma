namespace Olekstra.LikePharma.Client
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Обновление информации об аптеках сети.
    /// </summary>
    [XmlRoot("update_pharmacies_request")]
    public class UpdatePharmaciesRequest : RequestBase<UpdatePharmaciesResponse>
    {
        /// <summary>
        /// Способ обновления (по отношению к ранее переданным данным аптек).
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <listheader>Допустимые значения</listheader>
        /// <item><see cref="Globals.UpdateModeMerge"/> - оставить все ранее переданные аптеки, не упомянутые в данном запросе, без изменений</item>
        /// <item><see cref="Globals.UpdateModeReplace"/> - удалить все ранее переданные аптеки, не присутствующие в данном запросе</item>
        /// </list></remarks>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [UpdateMode(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.UpdateModeInvalid))]
        [JsonPropertyName("update_mode")]
        [XmlElement("update_mode")]
        public string? UpdateMode { get; set; }

        /// <summary>
        /// Список аптек.
        /// </summary>
        [NonEmptyCollection]
        [JsonPropertyName("pharmacies")]
        [XmlArray("pharmacies")]
        [XmlArrayItem("pharmacy")]
        public List<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();

        /// <summary>
        /// Описание аптеки.
        /// </summary>
        public class Pharmacy
        {
            /// <summary>
            /// Идентификатор аптеки. Строка, уникальная внутри аптечной сети.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [PharmacyId]
            [JsonPropertyName("id")]
            [XmlElement("id")]
            public string? Id { get; set; }

            /// <summary>
            /// Бренд (наименование на вывеске).
            /// </summary>
            [JsonPropertyName("brand")]
            [XmlElement("brand")]
            public string? Brand { get; set; }

            /// <summary>
            /// Регион (субъект РФ), где находится аптека.
            /// </summary>
            [JsonPropertyName("region")]
            [XmlElement("region")]
            public string? Region { get; set; }

            /// <summary>
            /// Город (населенный пункт), где находится аптека.
            /// </summary>
            [JsonPropertyName("city")]
            [XmlElement("city")]
            public string? City { get; set; }

            /// <summary>
            /// Адрес аптеки (полный).
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("address")]
            [XmlElement("address")]
            public string? Address { get; set; }

            /// <summary>
            /// GPS-координаты аптеки (в градусах, например <c>55.753960, 37.620393</c>).
            /// </summary>
            [JsonPropertyName("gps")]
            [XmlElement("gps")]
            public string? Gps { get; set; }

            /// <summary>
            /// Часы работы.
            /// </summary>
            [JsonPropertyName("work_time")]
            [XmlElement("work_time")]
            public string? WorkTime { get; set; }

            /// <summary>
            /// Контактные телефоны (для покупателей).
            /// </summary>
            [JsonPropertyName("phones")]
            [XmlElement("phones")]
            public string? Phones { get; set; }

            /// <summary>
            /// Список идентификаторов POS-терминалов аптеки.
            /// </summary>
            [JsonPropertyName("pos_ids")]
            [XmlArray("pos_ids")]
            [XmlArrayItem("pos_id")]
            public List<string>? PosIDs { get; set; }

            /// <summary>
            /// Признак "отключенности" аптеки.
            /// </summary>
            [JsonPropertyName("disabled")]
            [XmlElement("disabled")]
            public bool Disabled { get; set; }
        }
    }
}
