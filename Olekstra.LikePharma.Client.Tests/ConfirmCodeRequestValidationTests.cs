namespace Olekstra.LikePharma.Client
{
    using System;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class ConfirmCodeRequestValidationTests
    {
        private readonly ConfirmCodeRequest validValue;

        private readonly LikePharmaValidator validator = new LikePharmaValidator(ProtocolSettings.CreateEmpty());

        public ConfirmCodeRequestValidationTests()
        {
            validValue = new ConfirmCodeRequest
            {
                PosId = "A12BC",
                Code = "12345",
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCode(string value)
        {
            validValue.Code = value;

            Assert.False(validator.TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
