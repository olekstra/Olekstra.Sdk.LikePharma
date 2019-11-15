namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class ShortRussianPhoneNumberValidatorTests
    {
        private readonly IPhoneNumberValidator validator = new ShortRussianPhoneNumberValidator();

        [Theory]
        [InlineData("1234567890")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidatePhoneNumber(value));
        }

        [Theory]
        [InlineData("123456789")] // мало цифр
        [InlineData("12345678901")] // много цифр
        [InlineData("+71234567890")] // есть +7
        [InlineData("(123) 456-78-90")] // разделители
        [InlineData("81234567890")] // есть 8
        public void FailedValidation(string value)
        {
            var result = validator.ValidatePhoneNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
