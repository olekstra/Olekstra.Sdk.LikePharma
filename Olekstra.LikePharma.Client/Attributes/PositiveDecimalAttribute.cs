namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значения <c>Price</c> (проверка что значение неотрицательное).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class PositiveDecimalAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is decimal decimalValue))
            {
                return false;
            }

            return decimalValue > 0;
        }
    }
}
