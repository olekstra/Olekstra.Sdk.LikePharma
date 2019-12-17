namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class ConfirmCodeRequestSerializationTests : SerializationTestsBase<ConfirmCodeRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""A123"",
""pharmacy_id"":""test_pharmacy"",
""code"":""12345""
}";

        private const string ValidXml = @"
<confirm_code_request>
<pos_id>A123</pos_id>
<pharmacy_id>test_pharmacy</pharmacy_id>
<code>12345</code>
</confirm_code_request>";

        public ConfirmCodeRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(ConfirmCodeRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("A123", value.PosId);
            Assert.Equal("test_pharmacy", value.PharmacyId);
            Assert.Equal("12345", value.Code);
        }
    }
}
