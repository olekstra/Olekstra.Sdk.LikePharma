namespace Olekstra.LikePharma.Client
{
    using System.ComponentModel.DataAnnotations;
    using Olekstra.LikePharma.Client.Validators;

    /// <summary>
    /// Вспомогательный класс для тестирования.
    /// </summary>
    public class DummyPhoneValidator : IPhoneNumberValidator
    {
        public DummyPhoneValidator(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }

        public string ValidatedValue { get; private set; }

        public ValidationResult ValidatePhoneNumber(string value)
        {
            ValidatedValue = value;
            return ValidationResult;
        }
    }
}
