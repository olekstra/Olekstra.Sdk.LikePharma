namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class GetDiscountResponseValidationTests : ResponseBaseValidationTests<GetDiscountResponse>
    {
        public GetDiscountResponseValidationTests()
        {
            ValidValue.PosId = PosIdAttributeTests.ValidPosIdValue;
            ValidValue.CardNumber = "12345";
            ValidValue.Orders = new List<GetDiscountResponse.Order>
            {
                new GetDiscountResponse.Order
                {
                    Barcode = "1234567890123",
                    Count = 7,
                    Description = "SKU name",
                    Discount = 110.11M,
                    ErrorCode = 0,
                    Message = "Hello, World!",
                    Transaction = "abc123def",
                    Type = Globals.DiscountTypePercent,
                    Value = 99.99M,
                    ValuePerItem = 1.01M,
                },
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            ValidValue.PosId = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            ValidValue.PosId = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            ProtocolSettings.CardNumberValidator = new DummyCardValidator(new ValidationResult("fail"));

            ValidValue.PhoneNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            ProtocolSettings.PhoneNumberValidator = new DummyPhoneValidator(new ValidationResult("fail"));

            ValidValue.CardNumber = null; // чтобы валидация "телефон или карта" не сработала

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutCardPhoneNumber()
        {
            ValidValue.CardNumber = null;
            ValidValue.PhoneNumber = null;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        /// <summary>
        /// Так как запрос может быть с пустым набором, см. <see cref="GetDiscountRequestValidationTests.NotFailsWithoutOrders"/>.
        /// </summary>
        [Fact]
        public void NotFailsWithoutOrders()
        {
            ValidValue.Orders.Clear();

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void ErrorWithoutOrdersIsOk()
        {
            ValidValue.Status = Globals.StatusError;
            ValidValue.ErrorCode = 777;
            ValidValue.Orders.Clear();

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void FailsOnNullOrder()
        {
            ValidValue.Orders[0] = null;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        /// <summary>
        /// Так как запрос может быть с пустым набором, см. <see cref="GetDiscountRequestValidationTests.NotFailsOnEmptyOrderBarcode(string)"/>.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NotFailsOnEmptyOrderBarcode(string value)
        {
            ValidValue.Orders[0].Barcode = value;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void ZeroOrderCountIsNotOK()
        {
            ValidValue.Orders[0].Count = 0;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderCount(int value)
        {
            ValidValue.Orders[0].Count = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact(Skip = "Неясно, правильно ли это - \"успешный расчёт\" нулевой скидки")]
        public void ZeroOrderDiscountIsOK()
        {
            ValidValue.Orders[0].Discount = 0;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderDiscount(decimal value)
        {
            ValidValue.Orders[0].Discount = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyOrderMessage(string value)
        {
            ValidValue.Orders[0].Message = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidType()
        {
            ValidValue.Orders[0].Type = OrderDiscountTypeAttributeTests.InvalidTypeValue;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void ZeroOrderValueIsOK()
        {
            ValidValue.Orders[0].Value = 0;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderValue(decimal value)
        {
            ValidValue.Orders[0].Value = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void ZeroOrderValuePerItemIsOK()
        {
            ValidValue.Orders[0].ValuePerItem = 0;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderValuePerItem(decimal value)
        {
            ValidValue.Orders[0].ValuePerItem = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnOrderWithoutTransaction(string value)
        {
            ValidValue.Orders[0].Transaction = value;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnOrderWithoutDiscount()
        {
            ValidValue.Orders[0].Discount = 0;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnUnsuccessfulOrderWithTransaction()
        {
            ValidValue.Orders[0].ErrorCode = 13;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnUnsuccessfulOrderWithDiscount()
        {
            ValidValue.Orders[0].ErrorCode = 13;

            Assert.False(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void UnsuccessfulOrderWithoutTransactionAdnDiscountisOK()
        {
            ValidValue.Orders[0].ErrorCode = 13;
            ValidValue.Orders[0].Transaction = null;
            ValidValue.Orders[0].Discount = 0;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }
    }
}
