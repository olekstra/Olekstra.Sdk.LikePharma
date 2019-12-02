namespace Olekstra.LikePharma.Client
{
    using System.Xml.Serialization;

    /// <summary>
    /// Ответ на запрос <see cref="CancelPurchaseRequest"/>.
    /// </summary>
    [XmlRoot("cancel_purchase_response")]
    public class CancelPurchaseResponse : ResponseBase
    {
        // Nothing
    }
}
