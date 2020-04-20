namespace Olekstra.LikePharma.Client.Internal
{
    using System;
    using System.Text.Json;
    using Xunit;

    public class GetProgramsResponseHelperSerializationTests
    {
        public const string ValidJsonBoth = @"
{
""program"":[
{
""code"":""code1s"",
""name"":""name1s""
},
{
""code"":""code2s"",
""name"":""name2s""
}],
""programs"":[
{
""code"":""code1p"",
""name"":""name1p""
},
{
""code"":""code2p"",
""name"":""name2p""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        public const string ValidJsonSingular = @"
{
""program"":[
{
""code"":""code1s"",
""name"":""name1s""
},
{
""code"":""code2s"",
""name"":""name2s""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        public const string ValidJsonPlural = @"
{
""programs"":[
{
""code"":""code1p"",
""name"":""name1p""
},
{
""code"":""code2p"",
""name"":""name2p""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        public const string ValidXmlBoth = @"
<get_programs_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<program>
<program><code>code1s</code><name>name1s</name></program>
<program><code>code2s</code><name>name2s</name></program>
</program>
<programs>
<program><code>code1p</code><name>name1p</name></program>
<program><code>code2p</code><name>name2p</name></program>
</programs>
</get_programs_response>";

        public const string ValidXmlSingular = @"
<get_programs_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<program>
<program><code>code1s</code><name>name1s</name></program>
<program><code>code2s</code><name>name2s</name></program>
</program>
</get_programs_response>";

        public const string ValidXmlPlural = @"
<get_programs_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<programs>
<program><code>code1p</code><name>name1p</name></program>
<program><code>code2p</code><name>name2p</name></program>
</programs>
</get_programs_response>";

        [Theory]
        [InlineData(ValidJsonBoth, true, true)]
        [InlineData(ValidJsonSingular, true, false)]
        [InlineData(ValidJsonPlural, false, true)]
        public void JsonTest(string json, bool expectNonEmptySingular, bool expectNonEmptyPlural)
        {
            var value = JsonSerializer.Deserialize<GetProgramsResponseHelper>(json, LikePharmaClientOptions.CreateDefaultJsonSerializerOptions());

            Assert.NotNull(value);

            ValidateObject(value, expectNonEmptySingular, expectNonEmptyPlural);

            var json2 = JsonSerializer.Serialize(value, LikePharmaClientOptions.CreateDefaultJsonSerializerOptions());
            Assert.Equal(Helper.ReformatJson(json), json2);
        }

        [Theory]
        [InlineData(ValidXmlBoth, true, true)]
        [InlineData(ValidXmlSingular, true, false)]
        [InlineData(ValidXmlPlural, false, true)]
        public void XmlTest(string xml, bool expectNonEmptySingular, bool expectNonEmptyPlural)
        {
            var value = Helper.DeserializeXml<GetProgramsResponseHelper>(xml);

            Assert.NotNull(value);

            ValidateObject(value, expectNonEmptySingular, expectNonEmptyPlural);

            var xml2 = Helper.SerializeXml(value);
            Assert.Equal(Helper.ReformatXml(xml), xml2);
        }

        private static void ValidateObject(GetProgramsResponseHelper value, bool expectNonEmptySingular, bool expectNonEmptyPlural)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            if (expectNonEmptySingular)
            {
                Assert.NotNull(value.ProgramsSingular);
                Assert.Equal(2, value.ProgramsSingular.Count);
                Assert.Equal("code1s", value.ProgramsSingular[0].Code);
                Assert.Equal("name1s", value.ProgramsSingular[0].Name);
                Assert.Equal("code2s", value.ProgramsSingular[1].Code);
                Assert.Equal("name2s", value.ProgramsSingular[1].Name);
            }
            else
            {
                Assert.Null(value.ProgramsSingular);
            }

            if (expectNonEmptyPlural)
            {
                Assert.NotNull(value.ProgramsPlural);
                Assert.Equal(2, value.ProgramsPlural.Count);
                Assert.Equal("code1p", value.ProgramsPlural[0].Code);
                Assert.Equal("name1p", value.ProgramsPlural[0].Name);
                Assert.Equal("code2p", value.ProgramsPlural[1].Code);
                Assert.Equal("name2p", value.ProgramsPlural[1].Name);
            }
            else
            {
                Assert.Null(value.ProgramsPlural);
            }
        }
    }
}