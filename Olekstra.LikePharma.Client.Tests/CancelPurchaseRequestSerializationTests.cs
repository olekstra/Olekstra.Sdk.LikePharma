namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class CancelPurchaseRequestSerializationTests : SerializationTestsBase<CancelPurchaseRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""test_pos"",
""card_number"":""1234567890"",
""phone_number"":""+79011234567890"",
""trust_key"":""secret"",
""transactions"":[
""a12345bcde"",
""a12345bcdef""
]
}";

        private const string ValidXml = @"
<cancel_purchase_request>
<pos_id>test_pos</pos_id>
<card_number>1234567890</card_number>
<phone_number>+79011234567890</phone_number>
<trust_key>secret</trust_key>
<transactions>
<transaction>a12345bcde</transaction>
<transaction>a12345bcdef</transaction>
</transactions>
</cancel_purchase_request>";

        public CancelPurchaseRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(CancelPurchaseRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("test_pos", value.PosId);
            Assert.Equal("1234567890", value.CardNumber);
            Assert.Equal("+79011234567890", value.PhoneNumber);
            Assert.Equal("secret", value.TrustKey);

            Assert.NotNull(value.Transactions);
            Assert.Equal(2, value.Transactions.Count);
            Assert.Equal("a12345bcde", value.Transactions[0]);
            Assert.Equal("a12345bcdef", value.Transactions[1]);
        }
    }
}
