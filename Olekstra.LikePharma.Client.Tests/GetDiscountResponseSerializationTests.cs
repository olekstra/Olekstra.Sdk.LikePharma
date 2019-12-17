namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetDiscountResponseSerializationTests : SerializationTestsBase<GetDiscountResponse>
    {
        private const string ValidJson = @"
{
""pos_id"":""A123"",
""pharmacy_id"":""test_pharmacy"",
""card_number"":""1234567890123456789"",
""phone_number"":""+71234567890"",
""any_data"":""Hello, AnyData!"",
""description"":""{description}"",
""orders"":[
{
""any_data"":""Hello everyone"",
""barcode"":""1234567890123"",
""count"":11,
""description"":""description_1"",
""discount"":110.11,
""error_code"":7,
""message"":""This is test message 1"",
""transaction"":""abc123def"",
""type"":""cash"",
""value"":99.99,
""value_per_item"":1.01
},
{
""any_data"":""Hello here"",
""barcode"":""1234567890000"",
""count"":22.5,
""description"":""description_2"",
""discount"":220.00,
""error_code"":77,
""message"":""This is test message 2"",
""transaction"":""abc456def"",
""type"":""percent"",
""value"":999.99,
""value_per_item"":1.00
}
],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private const string ValidXml = @"
<get_discount_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<pos_id>A123</pos_id>
<pharmacy_id>test_pharmacy</pharmacy_id>
<card_number>1234567890123456789</card_number>
<phone_number>+71234567890</phone_number>
<any_data>Hello, AnyData!</any_data>
<description>{description}</description>
<orders>
<order>
<any_data>Hello everyone</any_data>
<barcode>1234567890123</barcode>
<count>11</count>
<description>description_1</description>
<discount>110.11</discount>
<error_code>7</error_code>
<message>This is test message 1</message>
<transaction>abc123def</transaction>
<type>cash</type>
<value>99.99</value>
<value_per_item>1.01</value_per_item>
</order>
<order>
<any_data>Hello here</any_data>
<barcode>1234567890000</barcode>
<count>22.5</count>
<description>description_2</description>
<discount>220.00</discount>
<error_code>77</error_code>
<message>This is test message 2</message>
<transaction>abc456def</transaction>
<type>percent</type>
<value>999.99</value>
<value_per_item>1.00</value_per_item>
</order>
</orders>
</get_discount_response>";

        public GetDiscountResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetDiscountResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.Equal("A123", value.PosId);
            Assert.Equal("test_pharmacy", value.PharmacyId);
            Assert.Equal("1234567890123456789", value.CardNumber);
            Assert.Equal("+71234567890", value.PhoneNumber);
            Assert.Equal("Hello, AnyData!", value.AnyData);
            Assert.Equal("{description}", value.Description);
            Assert.NotNull(value.Orders);
            Assert.Equal(2, value.Orders.Count);

            var order = value.Orders[0];
            Assert.NotNull(order);
            Assert.Equal("Hello everyone", order.AnyData);
            Assert.Equal("1234567890123", order.Barcode);
            Assert.Equal(11, order.Count);
            Assert.Equal("description_1", order.Description);
            Assert.Equal(110.11M, order.Discount);
            Assert.Equal(7, order.ErrorCode);
            Assert.Equal("This is test message 1", order.Message);
            Assert.Equal("abc123def", order.Transaction);
            Assert.Equal("cash", order.Type);
            Assert.Equal(99.99M, order.Value);
            Assert.Equal(1.01M, order.ValuePerItem);

            order = value.Orders[1];
            Assert.NotNull(order);
            Assert.Equal("Hello here", order.AnyData);
            Assert.Equal("1234567890000", order.Barcode);
            Assert.Equal(22.5M, order.Count);
            Assert.Equal("description_2", order.Description);
            Assert.Equal(220.00M, order.Discount);
            Assert.Equal(77, order.ErrorCode);
            Assert.Equal("This is test message 2", order.Message);
            Assert.Equal("abc456def", order.Transaction);
            Assert.Equal("percent", order.Type);
            Assert.Equal(999.99M, order.Value);
            Assert.Equal(1.0M, order.ValuePerItem);
        }
    }
}
