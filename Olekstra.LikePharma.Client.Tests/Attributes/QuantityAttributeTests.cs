namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using Xunit;

    public class QuantityAttributeTests
    {
        public const decimal InvalidValue = -1;

        [Fact]
        public void AcceptValidValues()
        {
            var attr = new NonNegativeIntegerAttribute();

            Assert.True(attr.IsValid(0));
            Assert.True(attr.IsValid(1));
            Assert.True(attr.IsValid(99999999));
        }

        [Fact]
        public void RejectInvalidValues()
        {
            var attr = new NonNegativeIntegerAttribute();
            Assert.False(attr.IsValid(InvalidValue));
            Assert.False(attr.IsValid(-1));
            Assert.False(attr.IsValid(-99999999));
        }
    }
}
