﻿namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Validators;

    /// <summary>
    /// Проверочный значения с помощью <see cref="ICardNumberValidator"/> указанного в <see cref="ProtocolSettings"/>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> сразу считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class CardNumberAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool RequiresValidationContext => true;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!(value is string stringValue))
            {
                throw new InvalidOperationException(ValidationMessages.CanValidateOnlyStringValue);
            }

            if (string.IsNullOrEmpty(stringValue))
            {
                return ValidationResult.Success;
            }

            var policy = (ProtocolSettings)validationContext.GetService(typeof(ProtocolSettings));
            if (policy == null)
            {
                throw new ApplicationException(ValidationMessages.ValidationPolicyNotFound);
            }

            return policy.CardNumberValidator == null
                ? ValidationResult.Success
                : policy.CardNumberValidator.ValidateCardNumber(stringValue);
        }
    }
}
