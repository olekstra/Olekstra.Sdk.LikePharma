namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetProductsResponseSerializationTests : SerializationTestsBase<GetProductsResponse>
    {
        private const string ValidJson = @"
{
""products"":[
{
""barcode"":""1234567890123"",
""description"":""description1""
},
{
""barcode"":""1234567890000"",
""description"":""description2""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private const string ValidXml = @"
<get_products_response>
<status>error</status>
<error_code>11</error_code>
<message>Hello, World!</message>
<products>
<product><barcode>1234567890123</barcode><description>description1</description></product>
<product><barcode>1234567890000</barcode><description>description2</description></product>
</products>
</get_products_response>";

        public GetProductsResponseSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetProductsResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.NotNull(value.Products);
            Assert.Equal(2, value.Products.Count);
            Assert.Equal("1234567890123", value.Products[0].Barcode);
            Assert.Equal("description1", value.Products[0].Description);
            Assert.Equal("1234567890000", value.Products[1].Barcode);
            Assert.Equal("description2", value.Products[1].Description);
        }
    }
}