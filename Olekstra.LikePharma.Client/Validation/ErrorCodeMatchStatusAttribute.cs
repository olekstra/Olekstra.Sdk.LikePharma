namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значений <c>ErrorCode</c> и <c>Status</c>.
    /// </summary>
    public class ErrorCodeMatchStatusAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool RequiresValidationContext => false;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(value is BaseResponse baseResponse))
            {
                return new ValidationResult(ValidationMessages.StatusSuccessMustHaveErrorCodeZero);
            }

            if (baseResponse.ErrorCode != Globals.ErrorCodeNoError && string.Equals(baseResponse.Status, Globals.StatusSuccess, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult(ValidationMessages.StatusSuccessMustHaveErrorCodeZero);
            }

            if (baseResponse.ErrorCode == Globals.ErrorCodeNoError && string.Equals(baseResponse.Status, Globals.StatusError, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult(ValidationMessages.StatusErrorMustHaveNonZeroErrorCode);
            }

            return ValidationResult.Success;
        }
    }
}
