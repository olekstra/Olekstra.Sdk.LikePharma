namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class ConfirmCodeRequestValidationTests
    {
        private readonly ConfirmCodeRequest validValue;

        private readonly List<ValidationResult> results = new List<ValidationResult>();

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyCode(string value)
        {
            validValue.Code = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }
    }
}
