namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class RegisterResponseValidationTests : BaseResponseValidationTests<RegisterResponse>
    {
        public RegisterResponseValidationTests()
        {
            ValidValue.Code = "12345";
        }

        [Fact]
        public void ValidatesOkWithoutCode()
        {
            ValidValue.Code = null;

            Assert.True(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }
    }
}
