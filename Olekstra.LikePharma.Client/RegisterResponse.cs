namespace Olekstra.LikePharma.Client
{
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Ответ на запрос <see cref="RegisterRequest"/>.
    /// </summary>
    [XmlRoot("register_response")]
    public class RegisterResponse : BaseResponse
    {
        /// <summary>
        /// Код подтверждения телефона (если был указан правильный <see cref="RegisterRequest.TrustKey"/>). Для привязки карты передайте этот код в метод confirm_code.
        /// </summary>
        [JsonPropertyName("code")]
        [XmlElement("code")]
        public string? Code { get; set; }
    }
}
