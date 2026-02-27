namespace OrdersService.Models;

/// <summary>
/// Представляет заказ в системе.
/// </summary>
public class OrderEntity
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование заказа.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Цена заказа.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; set; }
}