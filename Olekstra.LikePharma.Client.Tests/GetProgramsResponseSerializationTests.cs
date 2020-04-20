namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetProgramsResponseSerializationTests : SerializationTestsBase<GetProgramsResponse>
    {
        private const string ValidJson = Internal.GetProgramsResponseHelperSerializationTests.ValidJsonPlural;
        private const string ValidXml = Internal.GetProgramsResponseHelperSerializationTests.ValidXmlPlural;

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
            Assert.Equal("code1p", value.Programs[0].Code);
            Assert.Equal("name1p", value.Programs[0].Name);
            Assert.Equal("code2p", value.Programs[1].Code);
            Assert.Equal("name2p", value.Programs[1].Name);
        }
    }
}