namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Базовый класс для запросов, с фиксацией типа ответов.
    /// </summary>
    /// <typeparam name="TResponse">Тип ответа на данный запрос.</typeparam>
    public abstract class RequestBase<TResponse> : RequestBase
        where TResponse : ResponseBase
    {
        // Nothing
    }
}
