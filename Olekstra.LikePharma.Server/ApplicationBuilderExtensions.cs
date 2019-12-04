namespace Microsoft.AspNetCore.Builder
{
    using System;
    using Microsoft.AspNetCore.Http;
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
        /// <param name="options">Настройки обработчика вызовов API.</param>
        /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
        /// <returns>Ссылку на исходный <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder MapLikePharma<TUser>(this IApplicationBuilder app, PathString rootPath, LikePharmaMiddlewareOptions options)
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

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.Map(rootPath, builder => builder.UseMiddleware<LikePharmaMiddleware<TUser>>(options));

            return app;
        }

        /// <summary>
        /// Регистрация обработчика вызовов к API.
        /// </summary>
        /// <param name="app">Ссылка на <see cref="IApplicationBuilder"/>.</param>
        /// <param name="rootPath">Путь, по которому должен располагаться "корень" API (обычно <c>/api/1.0</c>).</param>
        /// <param name="optionsAction">Операции по дополнительной настройке обработчика вызовов API.</param>
        /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
        /// <returns>Ссылку на исходный <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder MapLikePharma<TUser>(this IApplicationBuilder app, PathString rootPath, Action<LikePharmaMiddlewareOptions> optionsAction)
            where TUser : class
        {
            var options = new LikePharmaMiddlewareOptions();
            optionsAction?.Invoke(options);

            return MapLikePharma<TUser>(app, rootPath, options);
        }

        /// <summary>
        /// Регистрация обработчика вызовов к API.
        /// </summary>
        /// <param name="app">Ссылка на <see cref="IApplicationBuilder"/>.</param>
        /// <param name="rootPath">Путь, по которому должен располагаться "корень" API (обычно <c>/api/1.0</c>).</param>
        /// <typeparam name="TUser">Класс, описывающий авторизованного пользователя (аптечную сеть).</typeparam>
        /// <returns>Ссылку на исходный <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder MapLikePharma<TUser>(this IApplicationBuilder app, PathString rootPath)
            where TUser : class
        {
            return MapLikePharma<TUser>(app, rootPath, new LikePharmaMiddlewareOptions());
        }
    }
}
