namespace OrdersService.Contracts;

/// <summary>
/// Контракт события, публикуемого при создании заказа.
/// </summary>
public interface IOrderCreated
{
    /// <summary>
    /// Gets идентификатор созданного заказа.
    /// </summary>
    Guid OrderId { get; }
}