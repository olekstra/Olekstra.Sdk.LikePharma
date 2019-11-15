namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class FullRussianPhoneNumberValidatorTests
    {
        private readonly IPhoneNumberValidator validator = new FullRussianPhoneNumberValidator();

        [Theory]
        [InlineData("+71234567890")]
        public void SuccessfulValidation(string value)
        {
            Assert.Equal(ValidationResult.Success, validator.ValidatePhoneNumber(value));
        }

        [Theory]
        [InlineData("+7123456789")] // мало цифр
        [InlineData("+712345678901")] // много цифр
        [InlineData("71234567890")] // нет плюса
        [InlineData("1234567890")] // нет +7
        [InlineData("+7 (123) 456-78-90")] // разделители
        [InlineData("81234567890")] // 8 вместо +7
        public void FailedValidation(string value)
        {
            var result = validator.ValidatePhoneNumber(value);
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
