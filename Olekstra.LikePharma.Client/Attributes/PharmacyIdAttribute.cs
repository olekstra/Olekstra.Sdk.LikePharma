namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Проверочный атрибут для для значения <c>PharmacyId</c>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class PharmacyIdAttribute : ValidationAttribute
    {
        private static readonly Regex ValidExpression = new Regex(
            @"^ [a-zA-Z\d][a-zA-Z\d\-]* $",
            RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <inheritdoc />
        public override bool RequiresValidationContext => true;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (!(value is string stringValue))
            {
                if (value != null)
                {
                    throw new InvalidOperationException(ValidationMessages.CanValidateOnlyStringValue);
                }
                else
                {
                    stringValue = string.Empty;
                }
            }

            var policy = (ProtocolSettings)validationContext.GetService(typeof(ProtocolSettings));
            if (policy == null)
            {
                throw new ApplicationException(ValidationMessages.ValidationPolicyNotFound);
            }

            var valueIsEmpty = string.IsNullOrWhiteSpace(stringValue);

            if (policy.PharmacyIdUsage == Usage.Required && valueIsEmpty)
            {
                return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PharmacyIdMustBeSet, validationContext.MemberName));
            }

            if (policy.PharmacyIdUsage == Usage.Forbidden && !valueIsEmpty)
            {
                return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PharmacyIdMustNotBeSet, validationContext.MemberName));
            }

            if (valueIsEmpty)
            {
                return ValidationResult.Success;
            }

            return ValidExpression.IsMatch(stringValue)
                ? ValidationResult.Success
                : new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PharmacyIdInvalid, validationContext.MemberName));
        }
    }
}
