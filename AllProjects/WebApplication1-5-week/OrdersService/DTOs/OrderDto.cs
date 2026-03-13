namespace OrdersService.DTOs;

using System.ComponentModel.DataAnnotations;
using OrdersService.Models;

/// <summary>
/// Модель заказа для API.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование заказа.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Цена заказа.
    /// </summary>
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; }
}