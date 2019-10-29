namespace Olekstra.LikePharma.Client
{
    using System.Xml.Serialization;

    /// <summary>
    /// Ответ на запрос <see cref="ConfirmCodeRequest"/>.
    /// </summary>
    [XmlRoot("confirm_code_response")]
    public class ConfirmCodeResponse : BaseResponse
    {
        // Nothing
    }
}
