namespace Olekstra.LikePharma.Client
{
    using System;
    using Olekstra.LikePharma.Client.Validators;

    /// <summary>
    /// Набор правил (политика) используемого протокола.
    /// </summary>
    public class ProtocolSettings
    {
        /// <summary>
        /// Валидатор для полей, содержащих номер телефона.
        /// </summary>
        public IPhoneNumberValidator? PhoneNumberValidator { get; set; }

        /// <summary>
        /// Валидатор для полей, содержащих номер карты.
        /// </summary>
        public ICardNumberValidator? CardNumberValidator { get; set; }

        /// <summary>
        /// Необходимость указания PharmacyId в запросах где указывается PosId.
        /// </summary>
        public Usage PharmacyIdUsage { get; set; } = Usage.Optional;

        /// <summary>
        /// Правила использования CardNumber / PhoneNumber в запросах.
        /// </summary>
        public CardAndPhoneUsage CardAndPhoneUsage { get; set; } = CardAndPhoneUsage.CardOrPhone;

        /// <summary>
        /// Создает "пустую" политику (без каких-либо валидаторов).
        /// </summary>
        /// <returns>Созданный объект <see cref="ProtocolSettings"/>.</returns>
        public static ProtocolSettings CreateEmpty()
        {
            return new ProtocolSettings
            {
                PhoneNumberValidator = null,
                CardNumberValidator = null,
                PharmacyIdUsage = Usage.Optional,
                CardAndPhoneUsage = CardAndPhoneUsage.CardOrPhone,
            };
        }

        /// <summary>
        /// Создает "стандартную" политику согласно протоколу на https://astrazeneca.like-pharma.com/api/documentation/.
        /// </summary>
        /// <returns>Созданный объект <see cref="ProtocolSettings"/>.</returns>
        public static ProtocolSettings CreateAstraZeneca()
        {
            return new ProtocolSettings
            {
                PhoneNumberValidator = new FullRussianPhoneNumberValidator(),
                CardNumberValidator = new Digit19CardNumberValidator(),
                PharmacyIdUsage = Usage.Forbidden,
                CardAndPhoneUsage = CardAndPhoneUsage.CardOrPhone,
            };
        }

        /// <summary>
        /// Создает "упрощённую" политику (pharmacy_id необязательный, номер карты допускается 13 или 19 цифр).
        /// </summary>
        /// <returns>Созданный объект <see cref="ProtocolSettings"/>.</returns>
        public static ProtocolSettings CreateOlekstra()
        {
            var p = CreateAstraZeneca();
            p.PharmacyIdUsage = Usage.Optional;
            p.CardNumberValidator = new Digit13Or19CardNumberValidator();
            return p;
        }
    }
}
