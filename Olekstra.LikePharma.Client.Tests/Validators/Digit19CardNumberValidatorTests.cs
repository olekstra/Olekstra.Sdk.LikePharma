namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class Digit19CardNumberValidatorTests
    {
        private readonly ICardNumberValidator validator = new Digit19CardNumberValidator();

        [Theory]
        [InlineData("1234567890123456789")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidateCardNumber(value));
        }

        [Theory]
        [InlineData("123456789012345678")] // мало цифр (18)
        [InlineData("123456789012344567890")] // много цифр (20)
        [InlineData("1234567890123456abc")] // есть буквы
        [InlineData("1234567 8901234 56789")] // есть разделители
        public void FailedValidation(string value)
        {
            var result = validator.ValidateCardNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
