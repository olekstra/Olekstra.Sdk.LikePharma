namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Проверка, что в список/коллекция не являются пустыми (содержат элементы).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NonEmptyCollectionAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool RequiresValidationContext => true;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (!(value is ICollection collection))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PropertyIsNotACollection, validationContext.MemberName));
            }

            if (collection.Count == 0)
            {
                return new ValidationResult(ValidationMessages.CollectionMustHaveElements);
            }

            foreach (var item in collection)
            {
                if (item == null)
                {
                    return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.CollectionCanNotHaveNullElements, validationContext.MemberName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
