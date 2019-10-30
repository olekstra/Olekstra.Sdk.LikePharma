namespace Olekstra.LikePharma.Client.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Проверочный атрибут для для значения <c>PhoneNumber</c>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private static readonly Regex ValidExpression = new Regex(
            @"^ \+ 7 [\d]{10} $",
            RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant | RegexOptions.Compiled);

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

            return string.IsNullOrEmpty(stringValue)
                ? true // обязательность значения должна проверяться другим атрибутом
                : ValidExpression.IsMatch(stringValue);
        }
    }
}
