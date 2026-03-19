namespace OrdersService.Models;

/// <summary>
/// Сущность заказа.
/// </summary>
public class OrderEntity
{
    /// <summary>
    /// Gets or sets идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets название заказа.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets стоимость заказа.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets статус заказа.
    /// </summary>
    public OrderStatus Status { get; set; }
}