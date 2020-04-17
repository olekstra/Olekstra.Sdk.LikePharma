namespace Olekstra.LikePharma.Client
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Xml;
    using System.Xml.Serialization;
    using Xunit;

    public abstract class SerializationTestsBase<T>
    {
        private readonly string json;

        private readonly string xml;

        private readonly string resultJson;

        private readonly string resultXml;

        public SerializationTestsBase(string json, string xml)
        {
            this.json = json ?? throw new ArgumentNullException(nameof(json));
            this.xml = xml ?? throw new ArgumentNullException(nameof(xml));

            this.resultJson = json
                .Replace("+", @"\u002B", StringComparison.Ordinal)
                .Replace("\r", string.Empty, StringComparison.Ordinal)
                .Replace("\n", string.Empty, StringComparison.Ordinal);

            this.resultXml = xml
                .Replace("\r", string.Empty, StringComparison.Ordinal)
                .Replace("\n", string.Empty, StringComparison.Ordinal);
        }

        public abstract void ValidateObject(T value);

        [Fact]
        public void JsonSerializationOk()
        {
            var value = JsonSerializer.Deserialize<T>(json, LikePharmaClientOptions.CreateDefaultJsonSerializerOptions());

            Assert.NotNull(value);

            ValidateObject(value);

            var json2 = JsonSerializer.Serialize(value, LikePharmaClientOptions.CreateDefaultJsonSerializerOptions());

            Assert.Equal(resultJson, json2);
        }

        [Fact]
        public void XmlSerializationOk()
        {
            var serializer = new XmlSerializer(typeof(T));

            using var sr = new StringReader(xml);
            using var xmlReader = XmlReader.Create(sr);
            var value = (T)serializer.Deserialize(xmlReader);

            Assert.NotNull(value);

            ValidateObject(value);

            using var ms = new MemoryStream();
            using var xw = XmlWriter.Create(ms, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false, Encoding = Encoding.ASCII });

            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            serializer.Serialize(xw, value, xmlns);

            var xml2 = Encoding.ASCII.GetString(ms.ToArray());

            Assert.Equal(resultXml, xml2);
        }
    }
}
