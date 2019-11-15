namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class Digit19Starting610294CardNumberValidatorTests
    {
        private readonly ICardNumberValidator validator = new Digit19Starting610294CardNumberValidator();

        [Theory]
        [InlineData("6102947890123456789")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidateCardNumber(value));
        }

        [Theory]
        [InlineData("610294789012345678")] // мало цифр (18)
        [InlineData("610294789012344567890")] // много цифр (20)
        [InlineData("6102947890123456abc")] // есть буквы
        [InlineData("610294 78901234 56789")] // есть разделители
        [InlineData("010294 78901234 56789")] // некорректный префикс
        public void FailedValidation(string value)
        {
            var result = validator.ValidateCardNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
