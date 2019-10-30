namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class GetDiscountResponseValidationTests : BaseResponseValidationTests<GetDiscountResponse>
    {
        public GetDiscountResponseValidationTests()
        {
            ValidValue.PosId = Validation.PosIdAttributeTests.ValidPosIdValue;
            ValidValue.CardNumber = Validation.CardNumberAttributeTests.ValidCardNumber;
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
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            ValidValue.PosId = Validation.PosIdAttributeTests.InvalidPosIdValue;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            ValidValue.CardNumber = Validation.CardNumberAttributeTests.InvalidCardNumberValue;
            ValidValue.PhoneNumber = null;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            ValidValue.CardNumber = null;
            ValidValue.PhoneNumber = Validation.PhoneNumberAttributeTests.InvalidPhoneNumberValue;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnBothCardAndPhoneNumber()
        {
            ValidValue.CardNumber = Validation.CardNumberAttributeTests.ValidCardNumber;
            ValidValue.PhoneNumber = Validation.PhoneNumberAttributeTests.ValidPhoneNumberValue;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsWithoutCardPhoneNumber()
        {
            ValidValue.CardNumber = null;
            ValidValue.PhoneNumber = null;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsWithoutOrders()
        {
            ValidValue.Orders.Clear();
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnNullOrder()
        {
            ValidValue.Orders[0] = null;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyOrderBarcode(string value)
        {
            var order = ValidValue.Orders[0];
            order.Barcode = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void ZeroOrderCountIsNotOK()
        {
            var order = ValidValue.Orders[0];
            order.Count = 0;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderCount(int value)
        {
            var order = ValidValue.Orders[0];
            order.Count = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact(Skip = "Неясно, правильно ли это.")]
        public void ZeroOrderDiscountIsOK()
        {
            var order = ValidValue.Orders[0];
            order.Discount = 0;
            Assert.True(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.Empty(Results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderDiscount(decimal value)
        {
            var order = ValidValue.Orders[0];
            order.Discount = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyOrderMessage(string value)
        {
            var order = ValidValue.Orders[0];
            order.Message = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnInvalidType()
        {
            var order = ValidValue.Orders[0];
            order.Type = Validation.OrderDiscountTypeAttributeTests.InvalidTypeValue;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void ZeroOrderValueIsOK()
        {
            var order = ValidValue.Orders[0];
            order.Value = 0;
            Assert.True(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.Empty(Results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderValue(decimal value)
        {
            var order = ValidValue.Orders[0];
            order.Value = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void ZeroOrderValuePerItemIsOK()
        {
            var order = ValidValue.Orders[0];
            order.ValuePerItem = 0;
            Assert.True(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.Empty(Results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderValuePerItem(decimal value)
        {
            var order = ValidValue.Orders[0];
            order.ValuePerItem = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnOrderWithoutTransaction(string value)
        {
            var order = ValidValue.Orders[0];
            order.Transaction = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnOrderWithoutDiscount()
        {
            var order = ValidValue.Orders[0];
            order.Discount = 0;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnUnsuccessfulOrderWithTransaction()
        {
            var order = ValidValue.Orders[0];
            order.ErrorCode = 13;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnUnsuccessfulOrderWithDiscount()
        {
            var order = ValidValue.Orders[0];
            order.ErrorCode = 13;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void UnsuccessfulOrderWithoutTransactionAdnDiscountisOK()
        {
            var order = ValidValue.Orders[0];
            order.ErrorCode = 13;
            order.Transaction = null;
            order.Discount = 0;
            Assert.True(Validator.TryValidateObject(order, new ValidationContext(order), Results, true));
            Assert.Empty(Results);
        }
    }
}
