namespace Olekstra.LikePharma.Client.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Проверочный атрибут для для значения <c>CardNumber</c>.
    /// </summary>
    /// <remarks>Значения <c>null</c> и <see cref="string.Empty"/> считаются "правильными" (проверку обязательности делайте отдельным <see cref="RequiredAttribute"/>).</remarks>
    public class CardNumberAttribute : ValidationAttribute
    {
        private static readonly Regex ValidExpression = new Regex(
            @"^ [\d]{19} $",
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
