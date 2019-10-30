namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using Xunit;

    public class PriceAttributeTests
    {
        public const decimal InvalidPriceValue = -1M;

        [Fact]
        public void AcceptValidValues()
        {
            var attr = new NonNegativeDecimalAttribute();

            Assert.True(attr.IsValid(0M));
            Assert.True(attr.IsValid(1M));
            Assert.True(attr.IsValid(0.000001M));
            Assert.True(attr.IsValid(1.999999M));
            Assert.True(attr.IsValid(99999999M));
        }

        [Fact]
        public void RejectInvalidValues()
        {
            var attr = new NonNegativeDecimalAttribute();
            Assert.False(attr.IsValid(InvalidPriceValue));
            Assert.False(attr.IsValid(-1M));
            Assert.False(attr.IsValid(-0.000001M));
            Assert.False(attr.IsValid(-1.999999M));
            Assert.False(attr.IsValid(-99999999M));
        }
    }
}
