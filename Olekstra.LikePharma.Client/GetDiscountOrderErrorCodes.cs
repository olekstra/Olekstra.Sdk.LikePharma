namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Предопределённые коды ошибок (error_code) для <see cref="GetDiscountResponse.Order"/>.
    /// </summary>
    public static class GetDiscountOrderErrorCodes
    {
        /// <summary>
        /// Указанный штрихкод не найден (не входит в дисконтную программу).
        /// </summary>
        public const int UnknownBarcode = 1;

        /// <summary>
        /// Штрихкод не был указан (в запросе), скидка не рассчитывалась.
        /// </summary>
        public const int EmptyBarcode = 101;

        /// <summary>
        /// Указанный штрихкод входит в дисконтную программу, не активную (не применимую) к данной аптеке.
        /// </summary>
        public const int NotInThisPharmacy = 10;

        /// <summary>
        /// Предельно допустимое количество уже было выкуплено,
        /// необходимо подождать некоторое время (до начала нового периода),
        /// в сообщении будет указана конкретная дата.
        /// </summary>
        public const int Overlimit = 3;

        /// <summary>
        /// Предельно допустимое количество уже было выкуплено, больше скидка на данный препарат по данной карте предоставляться не будет.
        /// </summary>
        public const int Redeemed = 301;
    }
}
