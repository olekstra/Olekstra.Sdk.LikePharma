namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class GetDiscountRequestValidationTests
    {
        private readonly GetDiscountRequest validValue;

        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public GetDiscountRequestValidationTests()
        {
            validValue = new GetDiscountRequest
            {
                PosId = Validation.PosIdAttributeTests.ValidPosIdValue,
                CardNumber = Validation.CardNumberAttributeTests.ValidCardNumber,
                AnyData = "Hello, World!",
                Orders = new List<GetDiscountRequest.Order>
                {
                    new GetDiscountRequest.Order
                    {
                        Barcode = "1234567890123",
                        Count = 11,
                        Price = 123.45M,
                        AnyData = "AnyData1",
                    },
                },
            };
        }

        [Fact]
        public void ValidatesOk()
        {
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

        [Fact]
        public void FailsOnInvalidCardNumber()
        {
            validValue.CardNumber = Validation.CardNumberAttributeTests.InvalidCardNumberValue;
            validValue.PhoneNumber = null;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnInvalidPhoneNumber()
        {
            validValue.CardNumber = null;
            validValue.PhoneNumber = Validation.PhoneNumberAttributeTests.InvalidPhoneNumberValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnBothCardAndPhoneNumber()
        {
            validValue.CardNumber = Validation.CardNumberAttributeTests.ValidCardNumber;
            validValue.PhoneNumber = Validation.PhoneNumberAttributeTests.ValidPhoneNumberValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsWithoutCardPhoneNumber()
        {
            validValue.CardNumber = null;
            validValue.PhoneNumber = null;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsWithoutOrders()
        {
            validValue.Orders.Clear();
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnNullOrder()
        {
            validValue.Orders[0] = null;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyOrderBarcode(string value)
        {
            var order = validValue.Orders[0];
            order.Barcode = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ZeroOrderCountIsNotOK()
        {
            var order = validValue.Orders[0];
            order.Count = 0;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderCount(int value)
        {
            var order = validValue.Orders[0];
            order.Count = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ZeroOrderPriceIsOK()
        {
            var order = validValue.Orders[0];
            order.Price = 0;
            Assert.True(Validator.TryValidateObject(order, new ValidationContext(order), results, true));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderPrice(decimal value)
        {
            var order = validValue.Orders[0];
            order.Price = value;
            Assert.False(Validator.TryValidateObject(order, new ValidationContext(order), results, true));
            Assert.NotEmpty(results);
        }
    }
}
