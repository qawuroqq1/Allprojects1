namespace DeliveryService.Contracts;

/// <summary>
/// Контракт события, получаемого при создании заказа.
/// Должен совпадать по структуре с событием из OrdersService.
/// </summary>
public interface IOrderCreated
{
    /// <summary>
    /// Идентификатор созданного заказа.
    /// </summary>
    Guid OrderId { get; }
}