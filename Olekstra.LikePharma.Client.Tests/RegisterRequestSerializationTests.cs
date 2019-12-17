namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class RegisterRequestSerializationTests : SerializationTestsBase<RegisterRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""A123"",
""pharmacy_id"":""test_pharmacy"",
""card_number"":""1234567890123456789"",
""phone_number"":""+71234567890"",
""trust_key"":""secret""
}";

        private const string ValidXml = @"
<register_request>
<pos_id>A123</pos_id>
<pharmacy_id>test_pharmacy</pharmacy_id>
<card_number>1234567890123456789</card_number>
<phone_number>+71234567890</phone_number>
<trust_key>secret</trust_key>
</register_request>";

        public RegisterRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(RegisterRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("A123", value.PosId);
            Assert.Equal("test_pharmacy", value.PharmacyId);
            Assert.Equal("1234567890123456789", value.CardNumber);
            Assert.Equal("+71234567890", value.PhoneNumber);
            Assert.Equal("secret", value.TrustKey);
        }
    }
}
