namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class Digit13CardNumberValidatorTests
    {
        private readonly ICardNumberValidator validator = new Digit13CardNumberValidator();

        [Theory]
        [InlineData("1234567890123")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidateCardNumber(value));
        }

        [Theory]
        [InlineData("123456789012")] // мало цифр (12)
        [InlineData("12345678901234")] // много цифр (14)
        [InlineData("1234567890abc")] // есть буквы
        [InlineData("1234567 890123")] // есть разделители
        public void FailedValidation(string value)
        {
            var result = validator.ValidateCardNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
