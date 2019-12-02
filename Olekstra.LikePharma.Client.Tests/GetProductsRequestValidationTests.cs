namespace Olekstra.LikePharma.Client
{
    using System;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class GetProductsRequestValidationTests
    {
        private readonly GetProductsRequest validValue;

        private readonly Policy policy = Policy.CreateEmpty();

        public GetProductsRequestValidationTests()
        {
            validValue = new GetProductsRequest
            {
                PosId = PosIdAttributeTests.ValidPosIdValue,
            };
        }

        [Fact]
        public void ValidatesOk()
        {
            Assert.True(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            validValue.PosId = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            validValue.PosId = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
