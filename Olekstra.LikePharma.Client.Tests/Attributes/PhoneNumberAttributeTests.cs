namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class PhoneNumberAttributeTests
    {
        [Fact]
        public void SuccessValidation()
        {
            var validator = new DummyPhoneValidator(ValidationResult.Success);
            var protocolSettings = new ProtocolSettings { PhoneNumberValidator = validator };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out var results);

            Assert.True(isValid); // потому что answer это Success;
            Assert.Equal(sample.SampleProperty, validator.ValidatedValue);
            Assert.Empty(results);
        }

        [Fact]
        public void SuccessValidationWithoutValidator()
        {
            var protocolSettings = new ProtocolSettings { PhoneNumberValidator = null };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out var results);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void FailedValidation()
        {
            var answer = new ValidationResult("abc");
            var validator = new DummyPhoneValidator(answer);
            var protocolSettings = new ProtocolSettings { PhoneNumberValidator = validator };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sample, out var results);

            Assert.False(isValid); // потому что answer это Success;
            Assert.Equal(sample.SampleProperty, validator.ValidatedValue);
            Assert.Single(results);
            Assert.Same(answer, results[0]);
        }

        private class SampleClass
        {
            [PhoneNumber]
            public string SampleProperty { get; set; }
        }
    }
}
