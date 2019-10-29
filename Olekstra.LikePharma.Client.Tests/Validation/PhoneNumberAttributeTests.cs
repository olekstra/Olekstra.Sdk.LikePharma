namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using Xunit;

    public class PhoneNumberAttributeTests
    {
        public const string ValidPhoneNumberValue = "+71234567890";

        public const string InvalidPhoneNumberValue = "+7 (123) 456-78-90";

        [Theory]
        [InlineData(ValidPhoneNumberValue)]
        public void AcceptValidValues(string value)
        {
            var attr = new PhoneNumberAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AcceptNullValues(string value)
        {
            var attr = new PhoneNumberAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(InvalidPhoneNumberValue)]
        [InlineData("71234567890")]
        [InlineData("81234567890")]
        [InlineData("+81234567890")]
        [InlineData("+7 123 456 78 90")]
        public void PhoneNumberAttribute(string value)
        {
            var attr = new PhoneNumberAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
