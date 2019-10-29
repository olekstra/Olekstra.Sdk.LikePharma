namespace Olekstra.LikePharma.Client
{
    using System.Text.Json;

    /// <summary>
    /// Глобальные константы API.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Значение свойства <c>Status</c> для случая "успех".
        /// </summary>
        public const string StatusSuccess = "success";

        /// <summary>
        /// Значение свойства <c>Status</c> для случая "ошибка".
        /// </summary>
        public const string StatusError = "error";

        /// <summary>
        /// Значение свойства <c>ErrorCode</c> для ситуаций, когда ошибок нет.
        /// </summary>
        public const int ErrorCodeNoError = 0;
    }
}
