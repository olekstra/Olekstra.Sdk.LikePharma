namespace Olekstra.LikePharma.Client
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Вспомогательный класс для преобразования (cast) <see cref="PhoneNumber"/> в строку и обратно.
    /// </summary>
    public class PhoneNumberTypeConverter : TypeConverter
    {
        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new PhoneNumber((string)value);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((PhoneNumber)value).ToString();
        }
    }
}
