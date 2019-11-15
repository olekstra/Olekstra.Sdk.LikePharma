namespace Olekstra.LikePharma.Client
{
    using System;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class ConfirmCodeRequestValidationTests
    {
        private readonly ConfirmCodeRequest validValue;

        private readonly Policy policy = Policy.CreateEmpty();

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
            Assert.True(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyPosId(string value)
        {
            validValue.PosId = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidPosId()
        {
            validValue.PosId = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCode(string value)
        {
            validValue.Code = value;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
