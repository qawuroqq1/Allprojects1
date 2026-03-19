using System.ComponentModel.DataAnnotations;
using OrdersService.Models;

namespace OrdersService.Domain.DTOs;

/// <summary>
/// Модель заказа для API.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Gets or sets идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets наименование заказа.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets цена заказа.
    /// </summary>
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; }
}