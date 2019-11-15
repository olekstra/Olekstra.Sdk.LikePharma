namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Валидатор, проверяющий что номер телефона указан в коротком виде: "10 цифр без разделителей".
    /// </summary>
    /// <remarks>Пустые значения считаются правильными, проверка заполненности должна делаться другими валидаторами.</remarks>
    public class ShortRussianPhoneNumberValidator : IPhoneNumberValidator
    {
        private static readonly Regex ValidExpression
            = new Regex(@"^ \d{10} $", RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        /// <inheritdoc />
        public ValidationResult ValidatePhoneNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ValidationResult.Success;
            }

            return ValidExpression.IsMatch(value)
                ? ValidationResult.Success
                : new ValidationResult(ValidationMessages.ShortRussianPhoneNumberValidator_Failure);
        }
    }
}
