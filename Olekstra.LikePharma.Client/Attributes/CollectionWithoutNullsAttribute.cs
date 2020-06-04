namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Проверка, что в список/коллекция не содержит пустые (null) элементы.
    /// </summary>
    /// <remarks>Сама по себе пустая (Count=0) или даже <b>null</b> коллекция считаются корректными.</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class CollectionWithoutNullsAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool RequiresValidationContext => true;

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            if (!(value is ICollection collection))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PropertyIsNotACollection, validationContext.MemberName));
            }

            foreach (var item in collection)
            {
                if (item == null)
                {
                    return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.CollectionCanNotHaveNullElements, validationContext.MemberName));
                }

                if (item is string itemString && string.IsNullOrWhiteSpace(itemString))
                {
                    return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.CollectionCanNotHaveNullElements, validationContext.MemberName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
