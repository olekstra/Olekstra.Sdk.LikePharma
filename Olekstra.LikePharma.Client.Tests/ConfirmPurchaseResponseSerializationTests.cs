namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class ConfirmPurchaseResponseSerializationTests : SerializationTestsBase<ConfirmPurchaseResponse>
    {
        private const string ValidJson = @"
{
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private const string ValidXml = @"
<confirm_purchase_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
</confirm_purchase_response>";

        public ConfirmPurchaseResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(ConfirmPurchaseResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);
        }
    }
}
