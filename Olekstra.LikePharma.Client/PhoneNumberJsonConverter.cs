namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Вспомогательный класс для преобразования (cast) <see cref="PhoneNumber"/> в JSON-строку и обратно.
    /// </summary>
    public class PhoneNumberJsonConverter : JsonConverter<PhoneNumber>
    {
        /// <inheritdoc />
        public override PhoneNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return PhoneNumber.Empty;
            }

            return new PhoneNumber(reader.GetString());
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, PhoneNumber value, JsonSerializerOptions options)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            writer.WriteStringValue(value.ToString());
        }
    }
}
