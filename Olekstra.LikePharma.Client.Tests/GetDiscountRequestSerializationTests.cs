namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetDiscountRequestSerializationTests : SerializationTestsBase<GetDiscountRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""A123"",
""pharmacy_id"":""test_pharmacy"",
""card_number"":""1234567890123456789"",
""phone_number"":""+71234567890"",
""any_data"":""Hello, World!"",
""orders"":[
{
""any_data"":""Hello, Order 1"",
""barcode"":""1234567890123"",
""count"":11,
""price"":123.45
},
{
""any_data"":""Hello, Order 2"",
""barcode"":""2222222222222"",
""count"":22.5,
""price"":222.22
}
]}";

        private const string ValidXml = @"
<get_discount_request>
<pos_id>A123</pos_id>
<pharmacy_id>test_pharmacy</pharmacy_id>
<card_number>1234567890123456789</card_number>
<phone_number>+71234567890</phone_number>
<any_data>Hello, World!</any_data>
<orders>
<order>
<any_data>Hello, Order 1</any_data>
<barcode>1234567890123</barcode>
<count>11</count>
<price>123.45</price>
</order>
<order>
<any_data>Hello, Order 2</any_data>
<barcode>2222222222222</barcode>
<count>22.5</count>
<price>222.22</price>
</order>
</orders>
</get_discount_request>";

        public GetDiscountRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetDiscountRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("A123", value.PosId);
            Assert.Equal("test_pharmacy", value.PharmacyId);
            Assert.Equal("1234567890123456789", value.CardNumber);
            Assert.Equal("+71234567890", value.PhoneNumber);
            Assert.Equal("Hello, World!", value.AnyData);
            Assert.NotNull(value.Orders);
            Assert.Equal(2, value.Orders.Count);

            var order = value.Orders[0];
            Assert.NotNull(order);
            Assert.Equal("1234567890123", order.Barcode);
            Assert.Equal(11M, order.Count);
            Assert.Equal(123.45M, order.Price);
            Assert.Equal("Hello, Order 1", order.AnyData);

            order = value.Orders[1];
            Assert.NotNull(order);
            Assert.Equal("2222222222222", order.Barcode);
            Assert.Equal(22.5M, order.Count);
            Assert.Equal(222.22M, order.Price);
            Assert.Equal("Hello, Order 2", order.AnyData);
        }
    }
}
