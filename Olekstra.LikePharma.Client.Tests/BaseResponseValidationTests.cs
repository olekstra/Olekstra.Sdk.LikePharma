namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
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

        protected Policy Policy { get; } = Policy.CreateEmpty();

        protected T ValidValue { get; } = new T();

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

            Assert.True(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyStatus(string value)
        {
            ValidValue.Status = value;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidStatus()
        {
            ValidValue.Status = StatusAttributeTests.InvalidStatusValue;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnNegativeErrorCode()
        {
            ValidValue.ErrorCode = -1;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsOnEmptyMessage(string value)
        {
            ValidValue.Message = value;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
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

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }
    }
}
