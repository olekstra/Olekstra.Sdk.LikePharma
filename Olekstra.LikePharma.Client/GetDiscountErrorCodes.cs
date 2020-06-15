namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Предопределённые коды ошибок (error_code) для <see cref="GetDiscountResponse"/>.
    /// </summary>
    public static class GetDiscountErrorCodes
    {
        /// <summary>
        /// Карта с указанным номером не найдена.
        /// </summary>
        public const int CardNotFound = 1;

        /// <summary>
        /// Карта с указанным найдена, но неактивна (не зарегистрирована или заблокирована).
        /// </summary>
        public const int CardDisabled = 101;

        /// <summary>
        /// Купон (одноразовый) уже был использован (погашен).
        /// </summary>
        public const int CouponAlreadyUsed = 4;

        /// <summary>
        /// Телефон не найден (среди зарегистрированных телефонов).
        /// </summary>
        public const int PhoneNotFound = 5;

        /// <summary>
        /// Нет скидок в чеке - параметры товарных позиций (количество, цена) не удовлетворяют требованиям программы.
        /// </summary>
        public const int NoDiscountsParamsNotValid = 6;

        /// <summary>
        /// Нет скидок в чеке - нет "нужных" товарных позиций (все имеющиеся - не входят в программу).
        /// </summary>
        public const int NoDiscountsUnknownBarcodes = 106;

        ////NEED_SMS_CONFIRM = 2, // требуется смс подтверждение
        ////CARD_NOT_LINK_PHONE_NUMBER = 3, // телефон не привязан к карте
    }
}
