namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Validators;

    /// <summary>
    /// Вспомогательный класс для тестирования.
    /// </summary>
    public class DummyCardValidator : ICardNumberValidator
    {
        public DummyCardValidator(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }

        public string ValidatedValue { get; private set; }

        public ValidationResult ValidateCardNumber(string value)
        {
            ValidatedValue = value;
            return ValidationResult;
        }
    }
}
