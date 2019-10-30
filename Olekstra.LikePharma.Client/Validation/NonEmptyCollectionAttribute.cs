namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверка, что в список/коллекция не являются пустыми (содержат элементы).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NonEmptyCollectionAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(value is ICollection collection))
            {
                throw new ArgumentException("Invalid value to validate: must implement ICollection", nameof(value));
            }

            if (collection.Count == 0)
            {
                return new ValidationResult(ValidationMessages.CollectionMustHaveElements);
            }

            foreach (var item in collection)
            {
                if (item == null)
                {
                    return new ValidationResult(ValidationMessages.CollectionCanNotHaveNullElements);
                }
            }

            return ValidationResult.Success;
        }
    }
}
