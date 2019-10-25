namespace Olekstra.LikePharma.Client
{
    using System;

    using Xunit;

    public class PhoneNumberTest
    {
        [Theory]
        [InlineData("+7 (903) 123-45-67")]
        [InlineData("+7-903-123-45-67")]
        [InlineData("+79031234567")]
        [InlineData("+7 903 123-45-67")]
        [InlineData("89031234567")]
        [InlineData("8 (903) 123-45-67")]
        [InlineData("8-903-123-45-67")]
        public void ValidatesCorrectUserPhone(string value)
        {
            var validStringValue = "+79031234567";

            Assert.Equal(validStringValue, new PhoneNumber(value).ToString());
            Assert.True(PhoneNumber.TryParse(value, out var phone));

            Assert.Equal(validStringValue, phone.ToString());
            Assert.False(phone.IsEmpty());
        }

        [Theory]
        [InlineData("03 123-45-67")]
        [InlineData("+779031234567")]
        [InlineData("889031234567")]
        [InlineData("+7 903 123-45-6a")]
        [InlineData(null)]
        public void NotValidatesIncorrectPhone(string value)
        {
            Assert.Throws<ArgumentException>(() => new PhoneNumber(value));

            Assert.False(PhoneNumber.TryParse(value, out var _));
        }

        [Theory]
        [InlineData("+79031234567", "+79031234567")]
        public void ConvertToString(string source, string result)
        {
            Assert.Equal(result, new PhoneNumber(source).ToString());
        }

        [Theory]
        [InlineData("+79031234567", "+7 (903) 123-45-67")]
        public void ConvertToBeautyPhone(string source, string result)
        {
            Assert.Equal(result, new PhoneNumber(source).ToBeautyPhone());
        }

        [Fact]
        public void EqualityTests()
        {
            var firstPhone = new PhoneNumber("+79031234567");
            var samePhone = new PhoneNumber("+79031234567"); // same as phone1
            var otherPhone = new PhoneNumber("+79031234568"); // different

            Assert.True(firstPhone.Equals(firstPhone)); // yes, compare to self, not a typo
            Assert.True(firstPhone.Equals(samePhone));
            Assert.True(samePhone.Equals(firstPhone));
            Assert.False(firstPhone.Equals(otherPhone));

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(firstPhone == firstPhone); // yes, compare to self, not a typo
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.True(firstPhone == samePhone);
            Assert.False(firstPhone == otherPhone);

            Assert.False(firstPhone != samePhone);
            Assert.True(firstPhone != otherPhone);
        }

        [Fact]
        public void DefaultTests()
        {
            var phone = default(PhoneNumber);
            Assert.Equal(PhoneNumber.Empty, phone);
            Assert.Equal("+70000000000", phone.ToString());
            Assert.Equal("+7 (000) 000-00-00", phone.ToBeautyPhone());
            Assert.True(phone.IsEmpty());
        }
    }
}
