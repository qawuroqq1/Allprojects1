namespace OrdersService.Contracts;

/// <summary>
/// Контракт события, получаемого при создании заказа.
/// </summary>
public interface IOrderCreated
{
    /// <summary>
    /// Идентификатор созданного заказа.
    /// </summary>
    Guid OrderId { get; }
}