namespace Olekstra.LikePharma.Client
{
    /// <summary>
    /// Базовый класс для всех запросов.
    /// </summary>
    /// <typeparam name="TResponse">Тип ответа на данный запрос.</typeparam>
    public abstract class RequestBase<TResponse>
        where TResponse : ResponseBase
    {
        // Nothing
    }
}
