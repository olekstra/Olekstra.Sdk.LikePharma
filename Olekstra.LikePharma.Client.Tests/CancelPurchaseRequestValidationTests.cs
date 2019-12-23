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

        private readonly Policy policy = Policy.CreateEmpty();

        public CancelPurchaseRequestValidationTests()
        {
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

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            policy.CardNumberValidator = new DummyCardValidator(new ValidationResult("fail"));

            validValue.PhoneNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            policy.PhoneNumberValidator = new DummyPhoneValidator(new ValidationResult("fail"));

            validValue.CardNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutCardPhoneNumber()
        {
            validValue.CardNumber = null;
            validValue.PhoneNumber = null;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyTrustKey(string value)
        {
            validValue.TrustKey = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutTransactions()
        {
            validValue.Transactions.Clear();

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyTransaction(string value)
        {
            validValue.Transactions[0] = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
