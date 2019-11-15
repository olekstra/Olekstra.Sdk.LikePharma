namespace Olekstra.LikePharma.Client
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
            };
        }
    }
}
