namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class RegisterRequestValidationTests
    {
        private readonly RegisterRequest validValue;

        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public RegisterRequestValidationTests()
        {
            validValue = new RegisterRequest
            {
                PosId = "A12BC",
                CardNumber = "1234567890123456789",
                PhoneNumber = "+71234567890",
                TrustKey = "abc",
            };
        }

        [Fact]
        public void ValidatesOk()
        {
            Assert.True(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.Empty(results);
        }

        [Fact]
        public void ValidatesOkWithoutTrustKey()
        {
            validValue.TrustKey = null;
            Assert.True(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            validValue.PosId = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            validValue.PosId = Validation.PosIdAttributeTests.InvalidPosIdValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCardNumber(string value)
        {
            validValue.CardNumber = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            validValue.CardNumber = Validation.CardNumberAttributeTests.InvalidCardNumberValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPhoneNumber(string value)
        {
            validValue.PhoneNumber = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            validValue.PhoneNumber = Validation.PhoneNumberAttributeTests.InvalidPhoneNumberValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }
    }
}
