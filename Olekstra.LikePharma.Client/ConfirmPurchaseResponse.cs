namespace Olekstra.LikePharma.Client
{
    using System.Xml.Serialization;

    /// <summary>
    /// Ответ на запрос <see cref="ConfirmPurchaseRequest"/>.
    /// </summary>
    [XmlRoot("confirm_purchase_response")]
    public class ConfirmPurchaseResponse : ResponseBase
    {
        // Nothing
    }
}
