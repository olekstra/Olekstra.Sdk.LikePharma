namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Валидатор, проверяющий что номер карты состоит из 13 цифр (проверка контрольного разряда не производится).
    /// </summary>
    /// <remarks>Пустые значения считаются правильными, проверка заполненности должна делаться другими валидаторами.</remarks>
    public class Digit13CardNumberValidator : ICardNumberValidator
    {
        /// <inheritdoc />
        public ValidationResult ValidateCardNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ValidationResult.Success;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference - выше value уже проверили на null.
            return (value.Length == 13 && value.All(c => c >= '0' && c <= '9'))
                ? ValidationResult.Success
                : new ValidationResult(ValidationMessages.Digit13CardNumberValidator_Failure);
        }
    }
}
