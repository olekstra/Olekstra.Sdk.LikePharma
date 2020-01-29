namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Attributes;
    using Xunit;

    public class UpdatePharmaciesRequestValidationTests
    {
        private readonly UpdatePharmaciesRequest validValue;

        private readonly Policy policy = Policy.CreateEmpty();

        public UpdatePharmaciesRequestValidationTests()
        {
            validValue = new UpdatePharmaciesRequest
            {
                UpdateMode = Globals.UpdateModeMerge,
                Pharmacies = new List<UpdatePharmaciesRequest.Pharmacy>()
                {
                    new UpdatePharmaciesRequest.Pharmacy
                    {
                        Id = "apt1",
                        Brand = "Sample",
                        Region = "Center",
                        City = "Moscow",
                        Address = "Central street, 1",
                        Gps = "55.55, 37.37",
                        WorkTime = "sometimes",
                        Phones = "Yes, we have",
                        Disabled = true,
                    },
                },
            };
        }

        [Fact]
        public void ValidatesOk()
        {
            Assert.True(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Empty(results);
        }

        [Fact]
        public void FailsOnInvalidUpdateMode()
        {
            validValue.UpdateMode = "foo";

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutPharmacies()
        {
            validValue.Pharmacies.Clear();

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnEmptyPharmacy()
        {
            validValue.Pharmacies[0] = null;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsOnInvalidId()
        {
            validValue.Pharmacies[0].Id = PosIdAttributeTests.InvalidPosIdValue;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutId()
        {
            validValue.Pharmacies[0].Id = null;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }

        [Fact]
        public void FailsWithoutAddress()
        {
            validValue.Pharmacies[0].Address = null;

            Assert.False(new LikePharmaValidator(policy).TryValidateObject(validValue, out var results));
            Assert.Single(results);
        }
    }
}
