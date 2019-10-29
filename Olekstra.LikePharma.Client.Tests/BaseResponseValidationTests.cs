namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class BaseResponseValidationTests
    {
        private readonly SampleResponse validValue;

        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public BaseResponseValidationTests()
        {
            validValue = MakeValidResponse(new SampleResponse());
        }

        public static T MakeValidResponse<T>(T value)
            where T : BaseResponse
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            value.Status = Globals.StatusSuccess;
            value.ErrorCode = Globals.ErrorCodeNoError;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            value.Message = "Hello, World";
#pragma warning restore CA1303 // Do not pass literals as localized parameters

            return value;
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ValidatesOk(bool invert)
        {
            if (invert)
            {
                validValue.Status = Globals.StatusError;
                validValue.ErrorCode = 123;
            }

            Assert.True(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyStatus(string value)
        {
            validValue.Status = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnInvalidStatus()
        {
            validValue.Status = Validation.StatusAttributeTests.InvalidStatusValue;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Fact]
        public void FailsOnNegativeErrorCode()
        {
            validValue.ErrorCode = -1;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyMessage(string value)
        {
            validValue.Message = value;
            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FailsOnStatusAndErrorCodeMismatch(bool invert)
        {
            if (invert)
            {
                validValue.Status = Globals.StatusError;
            }
            else
            {
                validValue.ErrorCode = 123;
            }

            Assert.False(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.NotEmpty(results);
        }

        private class SampleResponse : BaseResponse
        {
            // Nothing
        }
    }
}
