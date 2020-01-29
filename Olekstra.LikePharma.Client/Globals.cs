namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Глобальные константы API.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Значение свойства <see cref="ResponseBase.Status" /> для случая "успех".
        /// </summary>
        public const string StatusSuccess = "success";

        /// <summary>
        /// Значение свойства <see cref="ResponseBase.Status" /> для случая "ошибка".
        /// </summary>
        public const string StatusError = "error";

        /// <summary>
        /// Значение свойства <see cref="ResponseBase.ErrorCode" /> для ситуаций, когда ошибок нет.
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

        /// <summary>
        /// Имя HTTP-заголовка, содержащего логин (авторизационный идентификатор).
        /// </summary>
        public const string AuthorizationTokenHeaderName = "authorization-token";

        /// <summary>
        /// Имя HTTP-заголовка, содержащего пароль (авторизационный секрет).
        /// </summary>
        public const string AuthorizationSecretHeaderName = "authorization-secret";

        /// <summary>
        /// Значение свойства <see cref="UpdatePharmaciesRequest.UpdateMode" /> для варианта "слияние".
        /// </summary>
        public const string UpdateModeMerge = "merge";

        /// <summary>
        /// Значение свойства <see cref="UpdatePharmaciesRequest.UpdateMode" /> для варианта "замена".
        /// </summary>
        public const string UpdateModeReplace = "replace";
    }
}
