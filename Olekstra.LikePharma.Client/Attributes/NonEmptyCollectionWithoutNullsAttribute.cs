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
    public class NonEmptyCollectionWithoutNullsAttribute : CollectionWithoutNullsAttribute
    {
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
                return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.CollectionMustHaveElements, validationContext.MemberName));
            }

            if (!(value is ICollection collection))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ValidationMessages.PropertyIsNotACollection, validationContext.MemberName));
            }

            if (collection.Count == 0)
            {
                return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ValidationMessages.CollectionMustHaveElements, validationContext.MemberName));
            }

            // а сами элементы пусть проверяет предок
            return base.IsValid(value, validationContext);
        }
    }
}
