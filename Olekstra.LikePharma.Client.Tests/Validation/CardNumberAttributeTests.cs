namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using Xunit;

    public class CardNumberAttributeTests
    {
        public const string ValidCardNumber = "1234567890123456789";

        public const string InvalidCardNumberValue = "123456abc";

        [Theory]
        [InlineData(ValidCardNumber)]
        public void AcceptValidValues(string value)
        {
            var attr = new CardNumberAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AcceptNullValues(string value)
        {
            var attr = new CardNumberAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(InvalidCardNumberValue)]
        [InlineData("123456789012345678")]
        [InlineData("12345678901234567890")]
        [InlineData("123 456 789 012 345 6789")]
        [InlineData("12345678abc23456789")]
        public void RejectInvalidValues(string value)
        {
            var attr = new CardNumberAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
