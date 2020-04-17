namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class RegisterResponseValidationTests : ResponseBaseValidationTests<RegisterResponse>
    {
        public RegisterResponseValidationTests()
        {
            ValidValue.Code = "12345";
        }

        [Fact]
        public void ValidatesOkWithoutCode()
        {
            ValidValue.Code = null;

            Assert.True(Validator.TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }
    }
}
