namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using Xunit;

    public class PosIdAttributeTests
    {
        public const string InvalidPosIdValue = "a b c";

        [Theory]
        [InlineData("ABC123")]
        [InlineData("123ABC")]
        [InlineData("A")]
        [InlineData("1")]
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
        [InlineData("ABC-123")]
        [InlineData("абв")]
        [InlineData("*")]
        public void RejectInvalidValues(string value)
        {
            var attr = new PosIdAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
