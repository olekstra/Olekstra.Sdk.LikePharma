namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using Xunit;

    public class PharmacyIdAttributeTests
    {
        private const string ValidPharmacyId = "12345";

        [Theory]
        [InlineData(ValidPharmacyId)]
        [InlineData("123-456")]
        [InlineData("A")]
        [InlineData("A-1")]
        public void SuccessValidationForCorrect(string value)
        {
            var protocolSettings = new ProtocolSettings { PharmacyIdUsage = Usage.Required };

            var sample = new SampleClass { SampleProperty = value };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out var results);

            Assert.True(isValid);
            Assert.Equal(sample.SampleProperty, value);
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("A$5")]
        public void FailedValidationForIncorrect(string value)
        {
            var protocolSettings = new ProtocolSettings { PharmacyIdUsage = Usage.Required };

            var sample = new SampleClass { SampleProperty = value };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out var results);

            Assert.False(isValid);
            Assert.Equal(sample.SampleProperty, value);
            Assert.Single(results);
        }

        [Fact]
        public void RequireValueWhenRequired()
        {
            var protocolSettings = new ProtocolSettings { PharmacyIdUsage = Usage.Required };

            var sample = new SampleClass { SampleProperty = ValidPharmacyId };
            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.True(isValid);

            sample = new SampleClass { SampleProperty = string.Empty };
            isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.False(isValid);
        }

        [Fact]
        public void RejectValueWhenForbidden()
        {
            var protocolSettings = new ProtocolSettings { PharmacyIdUsage = Usage.Forbidden };

            var sample = new SampleClass { SampleProperty = ValidPharmacyId };
            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.False(isValid);

            sample = new SampleClass { SampleProperty = string.Empty };
            isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.True(isValid);
        }

        [Fact]
        public void AcceptAnyWhenOptional()
        {
            var protocolSettings = new ProtocolSettings { PharmacyIdUsage = Usage.Optional };

            var sample = new SampleClass { SampleProperty = ValidPharmacyId };
            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.True(isValid);

            sample = new SampleClass { SampleProperty = string.Empty };
            isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out _);
            Assert.True(isValid);
        }

        private class SampleClass
        {
            [PharmacyId]
            public string SampleProperty { get; set; }
        }
    }
}
