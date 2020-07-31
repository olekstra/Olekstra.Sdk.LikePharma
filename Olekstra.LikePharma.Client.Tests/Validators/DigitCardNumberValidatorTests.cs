namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class DigitCardNumberValidatorTests
    {
        private readonly ICardNumberValidator validator = new DigitCardNumberValidator();

        [Theory]
        [InlineData("1")]
        [InlineData("123")]
        [InlineData("1234567890123")]
        [InlineData("12345678901234567890")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidateCardNumber(value));
        }

        [Theory]
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
