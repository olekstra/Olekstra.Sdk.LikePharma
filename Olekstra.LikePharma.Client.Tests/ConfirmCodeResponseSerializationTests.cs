namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class ConfirmCodeResponseSerializationTests : SerializationTestsBase<ConfirmCodeResponse>
    {
        private const string ValidJson = @"
{
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private const string ValidXml = @"
<confirm_code_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
</confirm_code_response>";

        public ConfirmCodeResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(ConfirmCodeResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);
        }
    }
}
