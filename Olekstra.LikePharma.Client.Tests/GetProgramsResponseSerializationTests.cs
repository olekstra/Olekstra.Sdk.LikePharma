namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetProgramsResponseSerializationTests : SerializationTestsBase<GetProgramsResponse>
    {
        public const string ValidJson = LikePharmaClientTests_SmartDeserialize.ValidGetProgramsResponsePluralJson;

        private const string ValidXml = @"
<get_programs_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<programs>
<program><code>code1</code><name>name1</name></program>
<program><code>code2</code><name>name2</name></program>
</programs>
</get_programs_response>";

        public GetProgramsResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetProgramsResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.NotNull(value.Programs);
            Assert.Equal(2, value.Programs.Count);
            Assert.Equal("code1", value.Programs[0].Code);
            Assert.Equal("name1", value.Programs[0].Name);
            Assert.Equal("code2", value.Programs[1].Code);
            Assert.Equal("name2", value.Programs[1].Name);
        }
    }
}