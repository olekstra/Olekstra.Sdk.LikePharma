namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// "Стандартные" методы API Лайк-Фармы.
    /// </summary>
    public interface ILikePharmaClient
    {
        /// <summary>
        /// Выдача новой карты пациенту и привязка к ней номера телефона. Привязка существующей карты пациента к номеру телефона.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Подтверждение номера телефона по коду из СМС.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<ConfirmCodeResponse> ConfirmCodeAsync(ConfirmCodeRequest request);

        /// <summary>
        /// Запрос на расчёт снижения цены.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<GetDiscountResponse> GetDiscountAsync(GetDiscountRequest request);

        /// <summary>
        /// Подтверждение покупки.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<ConfirmPurchaseResponse> ConfirmPurchaseAsync(ConfirmPurchaseRequest request);

        /// <summary>
        /// Отмена покупки.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<CancelPurchaseResponse> CancelPurchaseAsync(CancelPurchaseRequest request);

        /// <summary>
        /// Получение списка активных продуктов в программах.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request);

        /// <summary>
        /// Получение списка активных программ.
        /// </summary>
        /// <param name="request">Параметры операции.</param>
        /// <returns>Результат операции.</returns>
        Task<GetProgramsResponse> GetProgramsAsync(GetProgramsRequest request);

        /// <summary>
        /// Десериализация JSON в класс запроса/ответа с учетом текущих настроек.
        /// </summary>
        /// <typeparam name="TValue">Результирующий тип (запроса или ответа).</typeparam>
        /// <param name="text">JSON текст.</param>
        /// <returns>Десериализованный экземпляр объекта.</returns>
        TValue DeserializeJson<TValue>(string text);

        /// <summary>
        /// Сериализация класса запроса/ответа в JSON с учетом текущих настроек.
        /// </summary>
        /// <typeparam name="TValue">Тип сериализуемого объекта (запроса или ответа).</typeparam>
        /// <param name="value">Сериализуемый объект.</param>
        /// <returns>Массив байтов JSON-сериализованного объекта.</returns>
        byte[] SerializeJsonUtf8Bytes<TValue>(TValue value);

        /// <summary>
        /// Сериализация класса запроса/ответа в JSON с учетом текущих настроек.
        /// </summary>
        /// <typeparam name="TValue">Тип сериализуемого объекта (запроса или ответа).</typeparam>
        /// <param name="value">Сериализуемый объект.</param>
        /// <returns>JSON-строка сериализованного объекта.</returns>
        string SerializeJson<TValue>(TValue value);
    }
}
