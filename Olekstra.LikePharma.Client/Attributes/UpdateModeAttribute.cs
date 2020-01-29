namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Проверочный атрибут для для значения <c>UpdateMode</c>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class UpdateModeAttribute : ValidationAttribute
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
                || string.CompareOrdinal(stringValue, Globals.UpdateModeMerge) == 0
                || string.CompareOrdinal(stringValue, Globals.UpdateModeReplace) == 0;
        }
    }
}
