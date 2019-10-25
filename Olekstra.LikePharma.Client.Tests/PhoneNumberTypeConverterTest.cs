namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel;
    using Xunit;

    public class PhoneNumberTypeConverterTest
    {
        [Fact]
        public void SerializeOk()
        {
            var converter = TypeDescriptor.GetConverter(typeof(PhoneNumber));

            var phone = new PhoneNumber("+7 900 123-45-67");
            var result = converter.ConvertToString(phone);

            // значение будет в кавычках - мы же сериализовываем объект в json, телефон это строковое значение!
            Assert.Equal("+79001234567", result);
        }

        [Fact]
        public void DeserializeOk()
        {
            var converter = TypeDescriptor.GetConverter(typeof(PhoneNumber));

            var value = "+79001234567";
            var phone = (PhoneNumber)converter.ConvertFrom(value);

            Assert.False(phone.IsEmpty());
            Assert.Equal("+79001234567", phone.ToString());
        }
    }
}
