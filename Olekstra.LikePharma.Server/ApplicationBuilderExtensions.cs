namespace Microsoft.AspNetCore.Builder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Olekstra.LikePharma.Client;
    using Olekstra.LikePharma.Server;

    /// <summary>
    /// Статический класс для регистрации и настройки серверной части АПИ.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Регистрация обработчика вызовов к API.
        /// </summary>
        /// <param name="app">Ссылка на <see cref="IApplicationBuilder"/>.</param>
        /// <param name="rootPath">Путь, по которому должен располагаться "корень" API (обычно <c>/api/1.0</c>).</param>
        /// <param name="policy">Политика проверки входящих запросов.</param>
        /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
        /// <returns>Ссылку на исходный <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder MapLikePharma<TUser>(this IApplicationBuilder app, PathString rootPath, Policy policy)
            where TUser : class
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (!rootPath.HasValue)
            {
                throw new ArgumentNullException(nameof(rootPath));
            }

            if (app == policy)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            app.Map(rootPath, builder => builder.UseMiddleware<LikePharmaMiddleware<TUser>>(policy));

            return app;
        }
    }
}
