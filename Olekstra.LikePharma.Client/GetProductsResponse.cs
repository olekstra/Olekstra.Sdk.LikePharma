namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Ответ на запрос <see cref="GetProductsRequest"/>.
    /// </summary>
    [XmlRoot("get_products_response")]
    public class GetProductsResponse : ResponseBase
    {
        /// <summary>
        /// Список активных продуктов.
        /// </summary>
        [EmptyCollectionWithoutEmptyElements]
        [JsonPropertyName("products")]
        [XmlArray("products")]
        [XmlArrayItem("product")]
        public List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Информация о программе.
        /// </summary>
        public class Product
        {
            /// <summary>
            /// Штрихкод (заводской штрихкод) препарата в программе.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("barcode")]
            [XmlElement("barcode")]
            public string? Barcode { get; set; }

            /// <summary>
            /// Наименование препарата.
            /// </summary>
            [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
            [JsonPropertyName("description")]
            [XmlElement("description")]
            public string? Description { get; set; }
        }
    }
}
