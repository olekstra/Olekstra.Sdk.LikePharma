namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Json;
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

            this.resultJson = Helper.ReformatJson(json);
            this.resultXml = Helper.ReformatXml(xml);
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
            var value = Helper.DeserializeXml<T>(xml);
            Assert.NotNull(value);

            ValidateObject(value);

            var xml2 = Helper.SerializeXml(value);
            Assert.Equal(resultXml, xml2);
        }
    }
}
