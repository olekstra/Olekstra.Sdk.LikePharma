namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Olekstra.LikePharma.Client;

    /// <summary>
    /// Интерфейс сервиса по обработке входящих запросов.
    /// </summary>
    /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
    public interface ILikePharmaService<TUser>
        where TUser : class
    {
        /// <summary>
        /// Проверка доступа (авторизация).
        /// </summary>
        /// <param name="authorizationToken">Значение заголовка <c>authorization-token</c>, переданное в запросе.</param>
        /// <param name="authorizationSecret">Значение заголовка <c>authorization-secret</c>, переданное в запросе.</param>
        /// <param name="httpRequest">Входящий запрос.</param>
        /// <returns>При успешной авторизации - идентификатор пользователя (сети), который впоследствии.</returns>
        /// <remarks>
        /// В случае успешной авторизации - метод должен вернуть не-null объект (пользователя, аптечную сеть),
        /// который впоследствии будет передан в параметр <c>user</c> методов обработки запросов.
        /// В случае неуспешной авторизации необходимо вернуть <c>null</c>, и тогда запрос не будет обрабатываться, клиенту вернется код ошибки 403 (Forbidden).
        /// </remarks>
        Task<TUser?> AuthorizeAsync(string authorizationToken, string authorizationSecret, HttpRequest httpRequest);

        /// <summary>
        /// Выдача новой карты пациенту и привязка к ней номера телефона, привязка существующей карты пациента к номеру телефона.
        /// </summary>
        /// <param name="request">Запрос на привязку номера телефона к карте пациента.</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (данные для завершения процесса выдачи/привязки).</returns>
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, TUser user);

        /// <summary>
        /// Подтверждение номера телефона по коду из СМС.
        /// </summary>
        /// <param name="request">Запрос на подтверждение кода.</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (был ли подтверждён номер телефона).</returns>
        Task<ConfirmCodeResponse> ConfirmCodeAsync(ConfirmCodeRequest request, TUser user);

        /// <summary>
        /// Запрос на расчёт снижения цены.
        /// </summary>
        /// <param name="request">Запрос (чек с перечнем товаров, количеств и цен и т.п.)</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (рассчитанные скидки).</returns>
        Task<GetDiscountResponse> GetDiscountAsync(GetDiscountRequest request, TUser user);

        /// <summary>
        /// Подтверждение покупки.
        /// </summary>
        /// <param name="request">Запрос (список подтверждаемых позиций по ранее рассчитанным скидкам).</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (подтверждение).</returns>
        Task<ConfirmPurchaseResponse> ConfirmPurchaseAsync(ConfirmPurchaseRequest request, TUser user);

        /// <summary>
        /// Отмена ранее подтверждённой покупки.
        /// </summary>
        /// <param name="request">Запрос (список отменяемых позиций из ранее подтверждённых).</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (подтверждение).</returns>
        Task<CancelPurchaseResponse> CancelPurchaseAsync(CancelPurchaseRequest request, TUser user);

        /// <summary>
        /// Запрос списка активных продуктов в программах.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (данные о товарных позициях).</returns>
        Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request, TUser user);

        /// <summary>
        /// Запрос списка активных программ.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (данные о активных программах).</returns>
        Task<GetProgramsResponse> GetProgramsAsync(GetProgramsRequest request, TUser user);

        /// <summary>
        /// Обновление списка аптек сети.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="user">Пользователь (аптечная сеть), ранее возвращенный методом <see cref="AuthorizeAsync(string, string, HttpRequest)"/>.</param>
        /// <returns>Результат операции (подтверждение).</returns>
        Task<UpdatePharmaciesResponse> UpdatePharmaciesAsync(UpdatePharmaciesRequest request, TUser user)
        {
            return Task.FromResult(new UpdatePharmaciesResponse { Status = Globals.StatusError, ErrorCode = 501, Message = Messages.Status501NotImplemented });
        }
    }
}
