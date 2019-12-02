namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Ответ на запрос <see cref="GetProgramsRequest"/>.
    /// </summary>
    [XmlRoot("get_programs_response")]
    public class GetProgramsResponse : ResponseBase
    {
        /// <summary>
        /// Список активных программ.
        /// </summary>
        [EmptyCollectionWithoutEmptyElements]
        [JsonPropertyName("program")]
        [XmlArray("program")]
        [XmlArrayItem("program")]
        public List<Program> Programs { get; set; } = new List<Program>();

        /// <summary>
        /// Информация о программе.
        /// </summary>
        public class Program
        {
            /// <summary>
            /// Название программы. Строка длиной до 100 символов. Используется для отображения названия на кассе.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("code")]
            [XmlElement("code")]
            public string? Code { get; set; }

            /// <summary>
            /// Код программы. Строка латиницей до 100 символов. Используется как уникальный код программы в системе.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("name")]
            [XmlElement("name")]
            public string? Name { get; set; }
        }
    }
}
