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
        /// Метод для регистрации обработчика вызовов к API.
        /// </summary>
        /// <param name="app">Ссылка на <see cref="IApplicationBuilder"/>.</param>
        /// <param name="rootPath">Путь, по которому должен располагаться "корень" API (обычно <c>/api/1.0</c>).</param>
        /// <returns>Ссылку на исходный <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseLikePharma(this IApplicationBuilder app, PathString rootPath)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (!rootPath.HasValue)
            {
                throw new ArgumentNullException(nameof(rootPath));
            }

            app.Map(rootPath, builder => builder.UseMiddleware<LikePharmaMiddleware>());

            return app;
        }
    }
}
