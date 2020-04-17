namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class RegisterRequestValidationTests
    {
        private readonly RegisterRequest validValue;

        private readonly ProtocolSettings protocolSettings;
        private readonly LikePharmaValidator validator;

        public RegisterRequestValidationTests()
        {
            this.protocolSettings = ProtocolSettings.CreateEmpty();
            this.validator = new LikePharmaValidator(protocolSettings);

            validValue = new RegisterRequest
            {
                PosId = "A12BC",
                CardNumber = "12345",
                PhoneNumber = "12345",
                TrustKey = "abc",
            };
        }

        [Fact]
        public void ValidatesOk()
        {
            Assert.True(validator.TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void ValidatesOkWithoutTrustKey()
        {
            validValue.TrustKey = null;

            Assert.True(validator.TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            validValue.PosId = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            validValue.PosId = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCardNumber(string value)
        {
            validValue.CardNumber = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            protocolSettings.CardNumberValidator = new DummyCardValidator(new ValidationResult("fail"));

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPhoneNumber(string value)
        {
            validValue.PhoneNumber = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            protocolSettings.PhoneNumberValidator = new DummyPhoneValidator(new ValidationResult("fail"));

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
