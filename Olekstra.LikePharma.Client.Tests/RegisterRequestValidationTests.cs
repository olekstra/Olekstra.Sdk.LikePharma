namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class RegisterRequestValidationTests
    {
        private readonly RegisterRequest validValue;

        private readonly Policy policy = Policy.CreateEmpty();

        public RegisterRequestValidationTests()
        {
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
            Assert.True(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void ValidatesOkWithoutTrustKey()
        {
            validValue.TrustKey = null;

            Assert.True(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            validValue.PosId = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            validValue.PosId = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCardNumber(string value)
        {
            validValue.CardNumber = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            policy.CardNumberValidator = new DummyCardValidator(new ValidationResult("fail"));

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPhoneNumber(string value)
        {
            validValue.PhoneNumber = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            policy.PhoneNumberValidator = new DummyPhoneValidator(new ValidationResult("fail"));

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
