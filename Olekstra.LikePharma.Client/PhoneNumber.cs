namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Примитивный тип "Номер телефона".
    /// </summary>
    [TypeConverter(typeof(PhoneNumberTypeConverter))]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public struct PhoneNumber : IComparable, IComparable<PhoneNumber>, IEquatable<PhoneNumber>
    {
        private const string EmptyPhone = "+70000000000";

        private static readonly Regex Pattern = new Regex(
            @"^\s* (?:\+7|8) [\s\-\(]* (\d{3}) [\)\s\-]* (\d{3}) [\s\-]* (\d{2}) [\-\s]* (\d{2}) \s* $",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

        private readonly string? number;

        /// <summary>
        /// Создает новый объект из строки.
        /// </summary>
        /// <param name="number">Строка, содержащая номер телефона в правильном формате.</param>
        /// <exception cref="ArgumentException">Если <paramref name="number"/> содержит недопустимое значение.</exception>
        public PhoneNumber(string number)
            : this(number, false)
        {
            // Nothing
        }

        private PhoneNumber(string number, bool skipValidation)
        {
            if (skipValidation)
            {
                this.number = number;
            }
            else
            {
                this.number = ToCanonicalNumber(number);
                if (this.number == null)
                {
                    throw new ArgumentException($"Not a valid phone number: '{number}'", nameof(number));
                }
            }
        }

        /// <summary>
        /// Возвращает "пустой" номер телефона (+7 и десять нулей).
        /// </summary>
        public static PhoneNumber Empty { get; } = new PhoneNumber(EmptyPhone, true);

        #region Equality and compare overloading, part 1

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) == 0;
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) != 0;
        }

        public static bool operator <(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator <=(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) <= 0;
        }

        public static bool operator >(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) > 0;
        }

        public static bool operator >=(PhoneNumber left, PhoneNumber right)
        {
            return Compare(left, right) >= 0;
        }

        #endregion

        public static bool TryParse(string value, out PhoneNumber result)
        {
            var canonical = ToCanonicalNumber(value);

            if (canonical == null)
            {
                result = PhoneNumber.Empty;
                return false;
            }

            result = new PhoneNumber(canonical, true);
            return true;
        }

        #region Equality and compare overloading, part 2

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is PhoneNumber p && Compare(this, p) == 0;
        }

        /// <inheritdoc />
        public bool Equals(PhoneNumber p)
        {
            return Compare(this, p) == 0;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (number ?? EmptyPhone).GetHashCode();
        }

        #endregion

        /// <summary>
        /// Преобразовывает данный номер телефона в "красивый" вид: <с>+7 (xxx) xxx-xx-xx</с>.
        /// </summary>
        /// <returns>Строка вида <c>+7 (xxx) xxx-xx-xx</c>.</returns>
        public string ToBeautyPhone()
        {
            var val = number ?? EmptyPhone;
            var group0 = val.Substring(2, 3);
            var group1 = val.Substring(5, 3);
            var group2 = val.Substring(8, 2);
            var group3 = val.Substring(10, 2);

            return $"+7 ({group0}) {group1}-{group2}-{group3}";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return number ?? EmptyPhone;
        }

        /// <summary>
        /// Проверяет, является ли данный номер телефона "пустой" (равен <see cref="EmptyPhone"/>).
        /// </summary>
        /// <returns><b>true</b> если номер "пустой", <b>false</b> в остальных случаях.</returns>
        public bool IsEmpty()
        {
            return number == null || string.Equals(number, EmptyPhone, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is PhoneNumber phoneNumber))
            {
                throw new ArgumentException("Argument must be PhoneNumber", nameof(obj));
            }

            return Compare(this, phoneNumber);
        }

        /// <inheritdoc />
        public int CompareTo(PhoneNumber other)
        {
            return Compare(this, other);
        }

        private static int Compare(PhoneNumber left, PhoneNumber right)
        {
            return string.CompareOrdinal(left.number ?? EmptyPhone, right.number ?? EmptyPhone);
        }

        private static string? ToCanonicalNumber(string? value)
        {
            if (value == null || string.IsNullOrEmpty(value))
            {
                return null;
            }

            var sb = new StringBuilder(20);
            foreach (var ch in value)
            {
                if (ch == '+' || (ch >= '0' && ch <= '9'))
                {
                    sb.Append(ch);
                }
            }

            var number = sb.ToString();

            var match = Pattern.Match(number);
            if (!match.Success)
            {
                return null;
            }

            var group0 = match.Groups[1].Value;
            var group1 = match.Groups[2].Value;
            var group2 = match.Groups[3].Value;
            var group3 = match.Groups[4].Value;

            return "+7" + group0 + group1 + group2 + group3;
        }
    }
}
