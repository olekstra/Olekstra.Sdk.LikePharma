namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Общий интерфейс для валидаторов номеров карт.
    /// </summary>
    public interface ICardNumberValidator
    {
        /// <summary>
        /// Валидация номера карты.
        /// </summary>
        /// <param name="value">Номер карты для валидации.</param>
        /// <returns>Результат валидации (<see cref="ValidationResult.Success"/> если ваидация успешна).</returns>
        ValidationResult ValidateCardNumber(string? value);
    }
}
