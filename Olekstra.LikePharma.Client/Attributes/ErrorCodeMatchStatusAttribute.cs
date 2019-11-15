namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значений <see cref="BaseResponse.ErrorCode"/> и <see cref="BaseResponse.Status"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
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
