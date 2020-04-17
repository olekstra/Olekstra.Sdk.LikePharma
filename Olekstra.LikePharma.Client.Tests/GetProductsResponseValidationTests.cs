namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using Xunit;

    public class GetProductsResponseValidationTests : ResponseBaseValidationTests<GetProductsResponse>
    {
        public GetProductsResponseValidationTests()
        {
            ValidValue.Products = new List<GetProductsResponse.Product>
            {
                new GetProductsResponse.Product
                {
                    Barcode = "1234567890123",
                    Description = "description1",
                },
                new GetProductsResponse.Product
                {
                    Barcode = "1234567890000",
                    Description = "description2",
                },
            };
        }

        [Fact]
        public void DoesNotFailWithoutProducts()
        {
            ValidValue.Products = null;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void DoesNotFailWithoutSkus2()
        {
            ValidValue.Products.Clear();

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void FailsOnNullSku()
        {
            ValidValue.Products[0] = null;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsWithProductWithoutBarcode(string value)
        {
            ValidValue.Products[0].Barcode = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsWithProductWithoutDescription(string value)
        {
            ValidValue.Products[0].Description = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }
    }
}
