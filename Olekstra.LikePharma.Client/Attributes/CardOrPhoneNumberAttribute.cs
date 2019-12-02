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

            var (card, phone) = value switch
            {
                GetDiscountRequest val => (val.CardNumber, val.PhoneNumber),
                GetDiscountResponse val => (val.CardNumber, val.PhoneNumber),
                ConfirmPurchaseRequest val => (val.CardNumber, val.PhoneNumber),
                CancelPurchaseRequest val => (val.CardNumber, val.PhoneNumber),
                _ => throw new ArgumentException(ValidationMessages.CardOrPhoneNumberAttribute_InvalidValue, nameof(value)),
            };

            var haveCard = !string.IsNullOrEmpty(card);
            var havePhone = !string.IsNullOrEmpty(phone);

            return haveCard == havePhone
                ? new ValidationResult(ValidationMessages.NeedEitherCardOrPhone)
                : ValidationResult.Success;
        }
    }
}
