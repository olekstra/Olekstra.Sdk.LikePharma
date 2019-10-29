namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class RegisterResponseSerializationTests : SerializationTestsBase<RegisterResponse>
    {
        private const string ValidJson = @"
{
""code"":""12345"",
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private const string ValidXml = @"
<register_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<code>12345</code>
</register_response>";

        public RegisterResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(RegisterResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);
            Assert.Equal("12345", value.Code);
        }
    }
}
