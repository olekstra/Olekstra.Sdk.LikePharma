namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Необходимость использования какого-либо поля.
    /// </summary>
    public enum Usage
    {
        /// <summary>
        /// Необходимость не указана (можно использовать, можно не использовать).
        /// </summary>
        Optional = 0,

        /// <summary>
        /// Поле необходимо, неуказание значения считается ошибкой.
        /// </summary>
        Required = 1,

        /// <summary>
        /// Поле запрещено. Указание какого-либо значения считается ошибкой.
        /// </summary>
        Forbidden = 2,
    }
}
