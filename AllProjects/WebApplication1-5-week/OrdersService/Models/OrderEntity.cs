namespace OrdersService.Models;

using System;

/// <summary>
/// Представляет заказ в системе.
/// </summary>
public class OrderEntity
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Цена заказа.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; set; }
}