namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значений параметров скидки внутри <see cref="GetDiscountResponse.Order"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ErrorCodeMatchDiscountAttribute : ValidationAttribute
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

            if (!(value is GetDiscountResponse.Order order))
            {
                throw new ArgumentException("Invalid value to validate: GetDiscountResponse.Order expected", nameof(value));
            }

            if (order.ErrorCode == Globals.ErrorCodeNoError)
            {
                if (string.IsNullOrWhiteSpace(order.Transaction))
                {
                    return new ValidationResult(ValidationMessages.SuccessfulOrderNeedTransaction);
                }

                if (order.Discount == 0M)
                {
                    return new ValidationResult(ValidationMessages.SuccessfulOrderNeedDiscount);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(order.Transaction))
                {
                    return new ValidationResult(ValidationMessages.FailedOrderNotNeedTransaction);
                }

                if (order.Discount > 0M)
                {
                    return new ValidationResult(ValidationMessages.FailedOrderNotNeedDiscount);
                }
            }

            return ValidationResult.Success;
        }
    }
}
