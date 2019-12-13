namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using Xunit;

    public class PosIdAttributeTests
    {
        public const string ValidPosIdValue = "ABC123";

        public const string InvalidPosIdValue = "a b c";

        [Theory]
        [InlineData(ValidPosIdValue)]
        [InlineData("123ABC")]
        [InlineData("A")]
        [InlineData("1")]
        [InlineData("1-23-456")]
        public void AcceptValidValues(string value)
        {
            var attr = new PosIdAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AcceptNullValues(string value)
        {
            var attr = new PosIdAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(InvalidPosIdValue)]
        [InlineData("ABC 123")]
        [InlineData("абв")]
        [InlineData("*")]
        public void RejectInvalidValues(string value)
        {
            var attr = new PosIdAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
