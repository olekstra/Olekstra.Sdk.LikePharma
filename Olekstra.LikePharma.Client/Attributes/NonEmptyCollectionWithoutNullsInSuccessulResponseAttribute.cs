namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Проверка, что в список/коллекция не являются пустыми (содержат элементы) - действует только положительных ответов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NonEmptyCollectionWithoutNullsInSuccessulResponseAttribute : NonEmptyCollectionWithoutNullsAttribute
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

            if (!(validationContext.ObjectInstance is ResponseBase responseValue))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ValidationMessages.ValidationContextObjectInstanceMustBeResponseBase, validationContext.MemberName));
            }

            if (responseValue.Status != Globals.StatusSuccess)
            {
                return ValidationResult.Success;
            }

            return base.IsValid(value, validationContext);
        }
    }
}
