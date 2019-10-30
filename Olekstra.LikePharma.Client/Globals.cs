namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Глобальные константы API.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Значение свойства <see cref="BaseResponse.Status" /> для случая "успех".
        /// </summary>
        public const string StatusSuccess = "success";

        /// <summary>
        /// Значение свойства <see cref="BaseResponse.Status" /> для случая "ошибка".
        /// </summary>
        public const string StatusError = "error";

        /// <summary>
        /// Значение свойства <see cref="BaseResponse.ErrorCode" /> для ситуаций, когда ошибок нет.
        /// </summary>
        public const int ErrorCodeNoError = 0;

        /// <summary>
        /// Значение свойства <see cref="GetDiscountResponse.Order.Type" /> для случая процентной скидки.
        /// </summary>
        public const string DiscountTypePercent = "percent";

        /// <summary>
        /// Значение свойства <see cref="GetDiscountResponse.Order.Type" /> для случая абсолютной (рублевой) скидки.
        /// </summary>
        public const string DiscountTypeAbsolute = "cash";
    }
}
