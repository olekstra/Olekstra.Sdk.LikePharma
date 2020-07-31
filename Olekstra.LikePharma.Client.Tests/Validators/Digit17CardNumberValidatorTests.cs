namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class Digit17CardNumberValidatorTests
    {
        private readonly ICardNumberValidator validator = new Digit17CardNumberValidator();

        [Theory]
        [InlineData("12345678901234567")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidateCardNumber(value));
        }

        [Theory]
        [InlineData("123456789012456")] // мало цифр (16)
        [InlineData("123456789012345678")] // много цифр (18)
        [InlineData("1234567890abc")] // есть буквы
        [InlineData("1234567 890123 4567")] // есть разделители
        public void FailedValidation(string value)
        {
            var result = validator.ValidateCardNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
