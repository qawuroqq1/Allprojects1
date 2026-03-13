namespace OrdersService.Models;

/// <summary>
/// Сущность заказа.
/// </summary>
public class OrderEntity
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название заказа.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Стоимость заказа.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}