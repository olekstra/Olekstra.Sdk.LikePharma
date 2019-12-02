namespace Olekstra.LikePharma.Client
{
    using System.Collections.Generic;
    using Xunit;

    public class GetProgramsResponseValidationTests : ResponseBaseValidationTests<GetProgramsResponse>
    {
        public GetProgramsResponseValidationTests()
        {
            ValidValue.Programs = new List<GetProgramsResponse.Program>
            {
                new GetProgramsResponse.Program
                {
                    Name = "name1",
                    Code = "code1",
                },
                new GetProgramsResponse.Program
                {
                    Name = "name2",
                    Code = "code2",
                },
            };
        }

        [Fact]
        public void DoesNotFailWithoutPrograms()
        {
            ValidValue.Programs = null;

            Assert.True(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void DoesNotFailWithoutSkus2()
        {
            ValidValue.Programs.Clear();

            Assert.True(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void FailsOnNullSku()
        {
            ValidValue.Programs[0] = null;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsWithProgramWithoutCode(string value)
        {
            ValidValue.Programs[0].Code = value;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" \t ")]
        public void FailsWithProgramWithoutName(string value)
        {
            ValidValue.Programs[0].Name = value;

            Assert.False(new LikePharmaValidator(Policy).TryValidateObject(ValidValue, out var results));
            Assert.Single(results);
        }
    }
}
