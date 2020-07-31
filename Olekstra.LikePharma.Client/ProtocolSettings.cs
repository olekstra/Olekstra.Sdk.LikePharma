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
        public IPhoneNumberValidator? PhoneNumberValidator { get; set; } = null;

        /// <summary>
        /// Валидатор для полей, содержащих номер карты.
        /// </summary>
        public ICardNumberValidator? CardNumberValidator { get; set; } = null;

        /// <summary>
        /// Необходимость указания PharmacyId в запросах где указывается PosId.
        /// </summary>
        public Usage PharmacyIdUsage { get; set; } = Usage.Optional;

        /// <summary>
        /// Правила использования CardNumber / PhoneNumber в запросах.
        /// </summary>
        public CardAndPhoneUsage CardAndPhoneUsage { get; set; } = CardAndPhoneUsage.CardOrPhone;

        /// <summary>
        /// Используется ли URL-кодирование для запросов (по умолчанию false).
        /// </summary>
        public bool UrlEncodedRequests { get; set; } = false;

        /// <summary>
        /// Используется ли URL-кодирование для ответов (по умолчанию false).
        /// </summary>
        public bool UrlEncodedResponses { get; set; } = false;

        /// <summary>
        /// Используется ли единственное число (program вместо programs) в ответе <see cref="GetProgramsResponse"/>.
        /// </summary>
        public bool SingularGetProgramsResponse { get; set; } = false;

        /// <summary>
        /// Создает "пустую" политику (без каких-либо валидаторов).
        /// </summary>
        /// <returns>Созданный объект <see cref="ProtocolSettings"/>.</returns>
        public static ProtocolSettings CreateEmpty()
        {
            return new ProtocolSettings();
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
                UrlEncodedRequests = true,
                UrlEncodedResponses = false,
                SingularGetProgramsResponse = true,
            };
        }

        /// <summary>
        /// Создает "упрощённую" политику (pharmacy_id необязательный, номер карты допускается 13 или 19 цифр, url-кодирования нет).
        /// </summary>
        /// <returns>Созданный объект <see cref="ProtocolSettings"/>.</returns>
        public static ProtocolSettings CreateOlekstra()
        {
            return new ProtocolSettings
            {
                PhoneNumberValidator = new FullRussianPhoneNumberValidator(),
                CardNumberValidator = new DigitCardNumberValidator(),
                PharmacyIdUsage = Usage.Optional,
                CardAndPhoneUsage = CardAndPhoneUsage.CardOrPhone,
                UrlEncodedRequests = false,
                UrlEncodedResponses = false,
                SingularGetProgramsResponse = false,
            };
        }

        /// <summary>
        /// Устанавливает свойство <see cref="PhoneNumberValidator"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UsePhoneNumberValidator(IPhoneNumberValidator? value)
        {
            this.PhoneNumberValidator = value;
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="CardNumberValidator"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UseCardNumberValidator(ICardNumberValidator? value)
        {
            this.CardNumberValidator = value;
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="PharmacyIdUsage"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UsePharmacyId(Usage value)
        {
            this.PharmacyIdUsage = value;
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="CardAndPhoneUsage"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UseCardAndPhone(CardAndPhoneUsage value)
        {
            this.CardAndPhoneUsage = value;
            return this;
        }

        /// <summary>
        /// Устанавливает свойства <see cref="UrlEncodedRequests"/> и <see cref="UrlEncodedResponses"/> в указанные значения.
        /// </summary>
        /// <param name="inRequests">Необходимое значение для <see cref="UrlEncodedRequests"/>.</param>
        /// <param name="inResponses">Необходимое значение для <see cref="UrlEncodedResponses"/>.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UseUrlEncode(bool inRequests, bool inResponses)
        {
            this.UrlEncodedRequests = inRequests;
            this.UrlEncodedResponses = inResponses;
            return this;
        }

        /// <summary>
        /// Устанавливает свойство <see cref="SingularGetProgramsResponse"/> в указанное значение.
        /// </summary>
        /// <param name="value">Необходимое значение.</param>
        /// <returns>Текущий экземпляр объекта.</returns>
        public ProtocolSettings UseSingularGetProgramsResponse(bool value)
        {
            this.SingularGetProgramsResponse = value;
            return this;
        }
    }
}
