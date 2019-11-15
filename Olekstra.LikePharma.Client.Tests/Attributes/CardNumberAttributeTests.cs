﻿namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class CardNumberAttributeTests
    {
        [Fact]
        public void SuccessValidation()
        {
            var validator = new DummyCardValidator(ValidationResult.Success);
            var policy = new Policy { CardNumberValidator = validator };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(policy).TryValidateObject(sample, out var results);

            Assert.True(isValid); // потому что answer это Success;
            Assert.Equal(sample.SampleProperty, validator.ValidatedValue);
            Assert.Empty(results);
        }

        [Fact]
        public void SuccessValidationWithoutValidator()
        {
            var policy = new Policy { CardNumberValidator = null };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(policy).TryValidateObject(sample, out var results);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void FailedValidation()
        {
            var answer = new ValidationResult("abc");
            var validator = new DummyCardValidator(answer);
            var policy = new Policy { CardNumberValidator = validator };

            var sample = new SampleClass { SampleProperty = "12345" };

            var isValid = new LikePharmaValidator(policy).TryValidateObject(sample, out var results);

            Assert.False(isValid); // потому что answer это Success;
            Assert.Equal(sample.SampleProperty, validator.ValidatedValue);
            Assert.Single(results);
            Assert.Same(answer, results[0]);
        }

        private class SampleClass
        {
            [CardNumber]
            public string SampleProperty { get; set; }
        }
    }
}
