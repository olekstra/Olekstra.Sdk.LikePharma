namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значения <c>Type</c>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class OrderDiscountTypeAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string stringValue))
            {
                return false;
            }

            return stringValue.Length == 0
                || string.CompareOrdinal(stringValue, Globals.DiscountTypeAbsolute) == 0
                || string.CompareOrdinal(stringValue, Globals.DiscountTypePercent) == 0;
        }
    }
}
