namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public abstract class BaseResponseValidationTests<T>
        where T : BaseResponse, new()
    {
        public BaseResponseValidationTests()
        {
            ValidValue.Status = Globals.StatusSuccess;
            ValidValue.ErrorCode = Globals.ErrorCodeNoError;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            ValidValue.Message = "Hello, World";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }

        protected T ValidValue { get; } = new T();

        protected List<ValidationResult> Results { get; } = new List<ValidationResult>();

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ValidatesOk(bool invert)
        {
            if (invert)
            {
                ValidValue.Status = Globals.StatusError;
                ValidValue.ErrorCode = 123;
            }

            Assert.True(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.Empty(Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyStatus(string value)
        {
            ValidValue.Status = value;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnInvalidStatus()
        {
            ValidValue.Status = Validation.StatusAttributeTests.InvalidStatusValue;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Fact]
        public void FailsOnNegativeErrorCode()
        {
            ValidValue.ErrorCode = -1;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyMessage(string value)
        {
            ValidValue.Message = value;
            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FailsOnStatusAndErrorCodeMismatch(bool invert)
        {
            if (invert)
            {
                ValidValue.Status = Globals.StatusError;
            }
            else
            {
                ValidValue.ErrorCode = 123;
            }

            Assert.False(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.NotEmpty(Results);
        }
    }
}
