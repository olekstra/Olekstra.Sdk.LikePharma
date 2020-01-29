namespace Olekstra.LikePharma.Client
{
    using System.Xml.Serialization;

    /// <summary>
    /// Ответ на запрос <see cref="UpdatePharmaciesRequest"/>.
    /// </summary>
    [XmlRoot("update_pharmacies_response")]
    public class UpdatePharmaciesResponse : ResponseBase
    {
        // Nothing
    }
}
