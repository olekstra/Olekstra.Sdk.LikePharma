namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
            Assert.True(Validator.TryValidateObject(ValidValue, new ValidationContext(ValidValue), Results, true));
            Assert.Empty(Results);
        }
    }
}
