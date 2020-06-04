namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class GetDiscountRequestValidationTests
    {
        private readonly GetDiscountRequest validValue;

        private readonly ProtocolSettings protocolSettings;
        private readonly LikePharmaValidator validator;

        public GetDiscountRequestValidationTests()
        {
            this.protocolSettings = ProtocolSettings.CreateEmpty();
            this.validator = new LikePharmaValidator(protocolSettings);

            validValue = new GetDiscountRequest
            {
                PosId = PosIdAttributeTests.ValidPosIdValue,
                CardNumber = "12345",
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

        /* 4 июня 2020:
         * поступила информация что лайк обрабатывает данный сценарий положительно (т.е. не считает за ошибку).
         * таким образом тест с отрицательного изменён на положительный
         */
        [Fact]
        public void NotFailsWithoutOrders()
        {
            validValue.Orders.Clear();

            Assert.True(validator.TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void FailsOnNullOrder()
        {
            validValue.Orders[0] = null;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        /* 4 июня 2020:
         * от аптек пришло пожелание разрешить данный вариант,
         * так как бывают "бонусные" элементы в чеке (пакеты,...) без заводских ШК
         */
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NotFailsOnEmptyOrderBarcode(string value)
        {
            validValue.Orders[0].Barcode = value;

            Assert.True(validator.TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void ZeroOrderCountIsNotOK()
        {
            validValue.Orders[0].Count = 0;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderCount(int value)
        {
            validValue.Orders[0].Count = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void ZeroOrderPriceIsOK()
        {
            validValue.Orders[0].Price = 0;

            Assert.True(validator.TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-999)]
        public void FailsOnWrongOrderPrice(decimal value)
        {
            validValue.Orders[0].Price = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
