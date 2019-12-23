﻿namespace Olekstra.LikePharma.Client
{
    using System;
    using Olekstra.LikePharma.Client.Validators;

    /// <summary>
    /// Набор правил (политика) валидации структур данных.
    /// </summary>
    public class Policy
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
        public Usage PharmacyIdUsage { get; set; }

        /// <summary>
        /// Правила использования CardNumber / PhoneNumber в запросах.
        /// </summary>
        public CardAndPhoneUsage CardAndPhoneUsage { get; set; } = CardAndPhoneUsage.CardOrPhone;

        /// <summary>
        /// Создает "пустую" политику (без каких-либо валидаторов).
        /// </summary>
        /// <returns>Созданный объект <see cref="Policy"/>.</returns>
        public static Policy CreateEmpty()
        {
            return new Policy();
        }

        /// <summary>
        /// Создает "стандартную" политику согласно протоколу на https://astrazeneca.like-pharma.com/api/documentation/.
        /// </summary>
        /// <returns>Созданный объект <see cref="Policy"/>.</returns>
        public static Policy CreateAstraZenecaPolicy()
        {
            return new Policy
            {
                PhoneNumberValidator = new FullRussianPhoneNumberValidator(),
                CardNumberValidator = new Digit19CardNumberValidator(),
                CardAndPhoneUsage = CardAndPhoneUsage.CardOrPhone,
            };
        }

        /// <summary>
        /// Создает "упрощённую" политику (номер карты допускается 13 или 19 цифр).
        /// </summary>
        /// <returns>Созданный объект <see cref="Policy"/>.</returns>
        public static Policy CreateOlekstraPolicy()
        {
            var p = CreateAstraZenecaPolicy();
            p.CardNumberValidator = new Digit13Or19CardNumberValidator();
            return p;
        }
    }
}
