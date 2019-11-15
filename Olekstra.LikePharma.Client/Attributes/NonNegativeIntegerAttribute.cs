namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значения <c>Count</c> (проверка что значение неотрицательное).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NonNegativeIntegerAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is int intValue))
            {
                return false;
            }

            return intValue >= 0;
        }
    }
}
