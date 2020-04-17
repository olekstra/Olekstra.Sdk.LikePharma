namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class CancelPurchaseRequestValidationTests
    {
        private readonly CancelPurchaseRequest validValue;

        private readonly ProtocolSettings protocolSettings;
        private readonly LikePharmaValidator validator;

        public CancelPurchaseRequestValidationTests()
        {
            this.protocolSettings = ProtocolSettings.CreateEmpty();
            this.validator = new LikePharmaValidator(protocolSettings);

            validValue = new CancelPurchaseRequest
            {
                PosId = "A12BC",
                CardNumber = "1234567890123",
                TrustKey = "secret",
                Transactions = new List<string>() { "abc123", "abcd1234" },
            };
        }

        [Fact]
        public void ValidatesOk()
        {
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

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            protocolSettings.CardNumberValidator = new DummyCardValidator(new ValidationResult("fail"));

            validValue.PhoneNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            protocolSettings.PhoneNumberValidator = new DummyPhoneValidator(new ValidationResult("fail"));

            validValue.CardNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutCardPhoneNumber()
        {
            validValue.CardNumber = null;
            validValue.PhoneNumber = null;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyTrustKey(string value)
        {
            validValue.TrustKey = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutTransactions()
        {
            validValue.Transactions.Clear();

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyTransaction(string value)
        {
            validValue.Transactions[0] = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
