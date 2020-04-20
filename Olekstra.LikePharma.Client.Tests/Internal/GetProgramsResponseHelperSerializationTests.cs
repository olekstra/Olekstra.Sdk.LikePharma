namespace Olekstra.LikePharma.Client.Internal
{
    using System;
    using Xunit;

    public class GetProgramsResponseHelperSerializationTests : SerializationTestsBase<GetProgramsResponseHelper>
    {
        private const string ValidJson = @"
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

        private const string ValidXml = @"
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

        public GetProgramsResponseHelperSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetProgramsResponseHelper value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.NotNull(value.ProgramsSingular);
            Assert.Equal(2, value.ProgramsSingular.Count);
            Assert.Equal("code1s", value.ProgramsSingular[0].Code);
            Assert.Equal("name1s", value.ProgramsSingular[0].Name);
            Assert.Equal("code2s", value.ProgramsSingular[1].Code);
            Assert.Equal("name2s", value.ProgramsSingular[1].Name);

            Assert.NotNull(value.ProgramsPlural);
            Assert.Equal(2, value.ProgramsPlural.Count);
            Assert.Equal("code1p", value.ProgramsPlural[0].Code);
            Assert.Equal("name1p", value.ProgramsPlural[0].Name);
            Assert.Equal("code2p", value.ProgramsPlural[1].Code);
            Assert.Equal("name2p", value.ProgramsPlural[1].Name);
        }
    }
}