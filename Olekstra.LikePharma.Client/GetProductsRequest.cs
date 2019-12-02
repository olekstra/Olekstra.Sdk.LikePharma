namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Запрос списка активных продуктов в программах.
    /// </summary>
    [XmlRoot("get_products_request")]
    public class GetProductsRequest : RequestBase<GetProductsResponse>
    {
        /// <summary>
        /// Идентификатор кассового терминала. Любая строка длиной до 40 символов, уникальная внутри аптечной сети.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.ValueRequired))]
        [PosId(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.PosIdInvalid))]
        [JsonPropertyName("pos_id")]
        [XmlElement("pos_id")]
        public string? PosId { get; set; }
    }
}
