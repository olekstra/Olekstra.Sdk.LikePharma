namespace Olekstra.LikePharma.Client
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class Helper
    {
        public static string ReformatJson(string source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));

            return source
                .Replace("+", @"\u002B", StringComparison.Ordinal)
                .Replace("\r", string.Empty, StringComparison.Ordinal)
                .Replace("\n", string.Empty, StringComparison.Ordinal);
        }

        public static string ReformatXml(string source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));

            return source
                .Replace("\r", string.Empty, StringComparison.Ordinal)
                .Replace("\n", string.Empty, StringComparison.Ordinal);
        }

        public static string SerializeXml<T>(T value)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var ms = new MemoryStream();
            using var xw = XmlWriter.Create(ms, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false, Encoding = Encoding.ASCII });

            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            serializer.Serialize(xw, value, xmlns);

            return Encoding.ASCII.GetString(ms.ToArray());
        }

        public static T DeserializeXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var sr = new StringReader(xml);
            using var xmlReader = XmlReader.Create(sr);

            return (T)serializer.Deserialize(xmlReader);
        }
    }
}
