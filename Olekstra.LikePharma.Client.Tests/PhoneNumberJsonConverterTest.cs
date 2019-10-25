namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Json;
    using Xunit;

    public class PhoneNumberJsonConverterTest
    {
        [Fact]
        public void SerializeOk()
        {
            var phone = new PhoneNumber("+7 900 123-45-67");
            var result = JsonSerializer.Serialize(phone);

            // значение будет в кавычках - мы же сериализовываем объект в json, телефон это строковое значение!
            Assert.Equal("\"\\u002B79001234567\"", result);
        }

        [Fact]
        public void DeserializeOk()
        {
            var value1 = "\"+79001234567\"";
            var phone1 = JsonSerializer.Deserialize<PhoneNumber>(value1);

            var value2 = "\"\\u002B79001234567\"";
            var phone2 = JsonSerializer.Deserialize<PhoneNumber>(value2);

            Assert.False(phone1.IsEmpty());
            Assert.Equal("+79001234567", phone1.ToString());
            Assert.False(phone2.IsEmpty());
            Assert.Equal("+79001234567", phone2.ToString());
        }
    }
}
