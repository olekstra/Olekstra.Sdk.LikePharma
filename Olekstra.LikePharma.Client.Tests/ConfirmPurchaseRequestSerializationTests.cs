namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class ConfirmPurchaseRequestSerializationTests : SerializationTestsBase<ConfirmPurchaseRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""test_pos"",
""card_number"":""1234567890"",
""phone_number"":""+79011234567890"",
""transactions"":[
""a12345bcde"",
""a12345bcdef""
],
""skus"":[
{
""barcode"":""1234567890123"",
""count"":3,
""price"":123.45
},
{
""barcode"":""1234567890000"",
""count"":33,
""price"":12345.00
}
]
}";

        private const string ValidXml = @"
<confirm_purchase_request>
<pos_id>test_pos</pos_id>
<card_number>1234567890</card_number>
<phone_number>+79011234567890</phone_number>
<transactions>
<transaction>a12345bcde</transaction>
<transaction>a12345bcdef</transaction>
</transactions>
<skus>
<sku><barcode>1234567890123</barcode><count>3</count><price>123.45</price></sku>
<sku><barcode>1234567890000</barcode><count>33</count><price>12345.00</price></sku>
</skus>
</confirm_purchase_request>";

        public ConfirmPurchaseRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(ConfirmPurchaseRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("test_pos", value.PosId);
            Assert.Equal("1234567890", value.CardNumber);
            Assert.Equal("+79011234567890", value.PhoneNumber);

            Assert.NotNull(value.Transactions);
            Assert.Equal(2, value.Transactions.Count);
            Assert.Equal("a12345bcde", value.Transactions[0]);
            Assert.Equal("a12345bcdef", value.Transactions[1]);

            Assert.NotNull(value.Skus);
            Assert.Equal(2, value.Skus.Count);
            Assert.Equal("1234567890123", value.Skus[0].Barcode);
            Assert.Equal(3, value.Skus[0].Count);
            Assert.Equal(123.45M, value.Skus[0].Price);

            Assert.Equal("1234567890000", value.Skus[1].Barcode);
            Assert.Equal(33, value.Skus[1].Count);
            Assert.Equal(12345M, value.Skus[1].Price);
        }
    }
}
