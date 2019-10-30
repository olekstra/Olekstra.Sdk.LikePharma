namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using Xunit;

    public class OrderDiscountTypeAttributeTests
    {
        public const string InvalidTypeValue = "Hello, World";

        [Theory]
        [InlineData(Globals.DiscountTypeAbsolute)]
        [InlineData(Globals.DiscountTypePercent)]
        public void AcceptValidValues(string value)
        {
            var attr = new OrderDiscountTypeAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AcceptNullValues(string value)
        {
            var attr = new OrderDiscountTypeAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(InvalidTypeValue)]
        public void RejectInvalidValues(string value)
        {
            var attr = new OrderDiscountTypeAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
