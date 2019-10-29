namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class RegisterResponseValidationTests
    {
        private readonly RegisterResponse validValue;

        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public RegisterResponseValidationTests()
        {
            validValue = BaseResponseValidationTests.MakeValidResponse(new RegisterResponse());
            validValue.Code = "12345";
        }

        [Fact]
        public void ValidatesOkWithoutCode()
        {
            validValue.Code = null;
            Assert.True(Validator.TryValidateObject(validValue, new ValidationContext(validValue), results, true));
            Assert.Empty(results);
        }
    }
}
