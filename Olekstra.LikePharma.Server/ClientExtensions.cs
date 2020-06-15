namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Linq;

    /// <summary>
    /// Дополнительные методы к классам Client-а.
    /// </summary>
    public static class ClientExtensions
    {
        /// <summary>
        /// Создает класс ответа без строк (orders),
        /// копируя из запроса в ответ те поля, которые должны вернуться неизменными,
        /// и устанавливая поля ErrorCode, Message и Description в указанные значения.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="errorCode">Значение для поля <see cref="ResponseBase.ErrorCode"/>.</param>
        /// <param name="message">Значение для поля <see cref="ResponseBase.Message"/>.</param>
        /// <param name="description">Значение для поля <see cref="GetDiscountResponse.Description"/>.</param>
        /// <returns>Ответ, заполненный данными из запроса (без копирования блока Orders).</returns>
        /// <remarks>Если передать значение 0 для <paramref name="errorCode"/>, то будет создан "успешный" ответ, иначе "ошибочный".</remarks>
        public static GetDiscountResponse CreateResponseWithoutOrders(
            this GetDiscountRequest request,
            int errorCode,
            string message,
            string? description = null)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            return new GetDiscountResponse
            {
                Status = errorCode == 0 ? Globals.StatusSuccess : Globals.StatusError,
                ErrorCode = errorCode,
                Message = message,
                PosId = request.PosId,
                PharmacyId = request.PharmacyId,
                CardNumber = request.CardNumber,
                PhoneNumber = request.PhoneNumber,
                AnyData = request.AnyData,
                Description = description,
                //// Orders = null,
            };
        }

        /// <summary>
        /// Создает класс ответа со строками (orders).
        /// Вначале вызывает метод <see cref="CreateResponseWithoutOrders(GetDiscountRequest, int, string, string?)"/> чтобы создать корневой объект.
        /// После этого копирует строки (orders), выставляя им статус Empty или NotFound в зависимости от заполненности поля <see cref="GetDiscountRequest.Order.Barcode"/>.
        /// Никакие скидки не рассчитываются, сохраняются исходные цены.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="orderMessageWhenEmptyBarcode">Сообщение для строк с пустым ШК.</param>
        /// <param name="orderMessageWhenUnknownBarcode">Сообщения для строк с непустым ШК.</param>
        /// <param name="errorCode">Значение для поля <see cref="ResponseBase.ErrorCode"/>.</param>
        /// <param name="message">Значение для поля <see cref="ResponseBase.Message"/>.</param>
        /// <param name="description">Значение для поля <see cref="GetDiscountResponse.Description"/>.</param>
        /// <returns>Ответ, заполненный данными из запроса (без скидок).</returns>
        public static GetDiscountResponse CreateResponseWithOrders(
            this GetDiscountRequest request,
            string orderMessageWhenEmptyBarcode,
            string orderMessageWhenUnknownBarcode,
            int errorCode,
            string message,
            string? description = null)
        {
            var resp = CreateResponseWithoutOrders(request, errorCode, message, description);

            if (string.IsNullOrEmpty(orderMessageWhenEmptyBarcode))
            {
                throw new ArgumentNullException(nameof(orderMessageWhenEmptyBarcode));
            }

            if (string.IsNullOrEmpty(orderMessageWhenUnknownBarcode))
            {
                throw new ArgumentNullException(nameof(orderMessageWhenUnknownBarcode));
            }

            resp.Orders = request.Orders
                .Select(x => new GetDiscountResponse.Order
                {
                    Barcode = x.Barcode,
                    Count = x.Count,
                    Price = x.Price,
                    AnyData = x.AnyData,
                    ErrorCode = string.IsNullOrEmpty(x.Barcode)
                            ? GetDiscountOrderErrorCodes.EmptyBarcode
                            : GetDiscountOrderErrorCodes.UnknownBarcode,
                    Message = string.IsNullOrEmpty(x.Barcode)
                            ? orderMessageWhenEmptyBarcode
                            : orderMessageWhenUnknownBarcode,
                    Discount = 0,
                    ValuePerItem = x.Price,
                    Value = x.Price * x.Count,
                })
                .ToList();

            return resp;
        }

        /// <summary>
        /// Сворачивает попозиционные errorCode и message в глобальные errorCode и message.
        /// </summary>
        /// <param name="response">Ответ, данные в котором надо свернуть.</param>
        /// <param name="messageWhenNoOrders">Сообщение для ситуации когда чек пустой.</param>
        /// <param name="messageWhenAllOrdersSuccessful">Сообщение для ситуации когда все строки со скидкой.</param>
        /// <param name="delimiter">Разделитель строк при сборке (по умолчанию <b>; \r\n</b>).</param>
        public static void RollupMessages(
            this GetDiscountResponse response,
            string messageWhenNoOrders,
            string messageWhenAllOrdersSuccessful,
            string? delimiter = "; \r\n")
        {
            response = response ?? throw new ArgumentNullException(nameof(response));

            if (response.Orders.Count == 0)
            {
                response.Status = Globals.StatusSuccess;
                response.ErrorCode = 0;
                response.Message = messageWhenNoOrders;
                return;
            }

            if (response.Orders.All(x => x.ErrorCode == 0))
            {
                response.Status = Globals.StatusSuccess;
                response.ErrorCode = 0;
                response.Message = messageWhenAllOrdersSuccessful;
                return;
            }

            response.Message = string.Join(
                delimiter,
                response.Orders.Select((x, i) => (string.IsNullOrEmpty(x.Description) ? (string.IsNullOrEmpty(x.Barcode) ? $"Строка {(i + 1)}" : x.Barcode) : x.Description) + ": " + x.Message));

            if (response.Orders.Any(x => x.ErrorCode == 0))
            {
                response.Status = Globals.StatusSuccess;
                response.ErrorCode = 0;
                return;
            }

            response.Status = Globals.StatusError;
            response.ErrorCode =
                response.Orders.All(x => x.ErrorCode == GetDiscountOrderErrorCodes.UnknownBarcode
                                      || x.ErrorCode == GetDiscountOrderErrorCodes.EmptyBarcode)
                ? GetDiscountErrorCodes.NoDiscountsUnknownBarcodes
                : GetDiscountErrorCodes.NoDiscountsParamsNotValid;
        }
    }
}
