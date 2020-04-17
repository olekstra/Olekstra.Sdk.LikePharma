namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Проверка, что в запросе заполнено ровно одно из полей <c>CardNumber</c> и <c>PhoneNumber</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CardOrPhoneNumberAttribute : ValidationAttribute
    {
        private const string CardNumberFieldName = "card_number";
        private const string PhoneNumberFieldName = "phone_number";

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

            var policy = (ProtocolSettings)validationContext.GetService(typeof(ProtocolSettings));
            if (policy == null)
            {
                throw new ApplicationException(ValidationMessages.ValidationPolicyNotFound);
            }

            switch (policy.CardAndPhoneUsage)
            {
                case CardAndPhoneUsage.CardOnly:
                    if (!haveCard)
                    {
                        return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.ValueRequired, CardNumberFieldName));
                    }

                    if (havePhone)
                    {
                        return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.ValueProhibited, PhoneNumberFieldName));
                    }

                    return ValidationResult.Success;

                case CardAndPhoneUsage.PhoneOnly:
                    if (!havePhone)
                    {
                        return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.ValueRequired, PhoneNumberFieldName));
                    }

                    if (haveCard)
                    {
                        return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.ValueProhibited, CardNumberFieldName));
                    }

                    return ValidationResult.Success;

                case CardAndPhoneUsage.CardOrPhone:
                    if (!haveCard && !havePhone)
                    {
                        return new ValidationResult(ValidationMessages.NeedCardOrPhone);
                    }

                    return ValidationResult.Success;

                case CardAndPhoneUsage.CardXorPhone:
                    if (haveCard == havePhone)
                    {
                        return new ValidationResult(ValidationMessages.NeedEitherCardOrPhone);
                    }

                    return ValidationResult.Success;

                case CardAndPhoneUsage.CardAndPhone:
                    if (!haveCard || !havePhone)
                    {
                        return new ValidationResult(ValidationMessages.NeedCardAndPhone);
                    }

                    return ValidationResult.Success;

                default:
                    throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, ValidationMessages.InvalidCardAndPhoneUsageValue, policy.CardAndPhoneUsage));
            }
        }
    }
}
