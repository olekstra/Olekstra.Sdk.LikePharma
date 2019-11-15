namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using Xunit;

    public class StatusAttributeTests
    {
        public const string InvalidStatusValue = "Hello, World";

        [Theory]
        [InlineData(Globals.StatusSuccess)]
        [InlineData(Globals.StatusError)]
        public void AcceptValidValues(string value)
        {
            var attr = new StatusAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AcceptNullValues(string value)
        {
            var attr = new StatusAttribute();
            Assert.True(attr.IsValid(value));
        }

        [Theory]
        [InlineData(InvalidStatusValue)]
        public void RejectInvalidValues(string value)
        {
            var attr = new StatusAttribute();
            Assert.False(attr.IsValid(value));
        }
    }
}
