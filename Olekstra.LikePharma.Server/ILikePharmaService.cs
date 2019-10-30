namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Интерфейс сервиса по обработке входящих запросов.
    /// </summary>
    public interface ILikePharmaService
    {
        /// <summary>
        /// Проверка доступа (авторизация).
        /// </summary>
        /// <param name="authorizationToken">Значение заголовка <c>authorization-token</c>, переданное в запросе.</param>
        /// <param name="authorizationSecret">Значение заголовка <c>authorization-secret</c>, переданное в запросе.</param>
        /// <returns>При успешной авторизации - идентификатор пользователя (сети), который впоследствии</returns>
        /// <remarks>
        /// В случае успешной авторизации - метод должен вернуть не-null иентификатор пользователя (аптечной сети),
        /// который впоследствии будет передан в параметре <c>userId</c> метода обработки запроса.
        /// В случае неуспешной авторизации необходимо вернуть <c>null</c>, и тогда запрос не будет обрабатываться, клиенту вернется код ошибки 403 (Forbidden).
        /// </remarks>
        Task<string?> AuthorizeAsync(string authorizationToken, string authorizationSecret);
    }
}
