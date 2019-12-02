namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверка, что в запросе заполнено ровно одно из полей (<see cref="CardNumber"/> и <see cref="PhoneNumber"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CardOrPhoneNumberAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is GetDiscountRequest getDiscountRequest)
            {
                var haveCard = !string.IsNullOrEmpty(getDiscountRequest.CardNumber);
                var havePhone = !string.IsNullOrEmpty(getDiscountRequest.PhoneNumber);

                return haveCard == havePhone
                    ? new ValidationResult(ValidationMessages.NeedEitherCardOrPhone)
                    : ValidationResult.Success;
            }

            if (value is GetDiscountResponse getDiscountResponse)
            {
                var haveCard = !string.IsNullOrEmpty(getDiscountResponse.CardNumber);
                var havePhone = !string.IsNullOrEmpty(getDiscountResponse.PhoneNumber);

                return haveCard == havePhone
                    ? new ValidationResult(ValidationMessages.NeedEitherCardOrPhone)
                    : ValidationResult.Success;
            }

            if (value is ConfirmPurchaseRequest confirmPurchaseRequest)
            {
                var haveCard = !string.IsNullOrEmpty(confirmPurchaseRequest.CardNumber);
                var havePhone = !string.IsNullOrEmpty(confirmPurchaseRequest.PhoneNumber);

                return haveCard == havePhone
                    ? new ValidationResult(ValidationMessages.NeedEitherCardOrPhone)
                    : ValidationResult.Success;
            }

            throw new ArgumentException(ValidationMessages.CardOrPhoneNumberAttribute_InvalidValue, nameof(value));
        }
    }
}
