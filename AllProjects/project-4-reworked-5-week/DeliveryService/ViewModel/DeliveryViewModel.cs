using System;

namespace DeliveryService.ViewModels;

/// <summary>
/// Модель представления доставки, возвращаемая клиенту.
/// </summary>
public class DeliveryViewModel
{
    /// <summary>
    /// Уникальный идентификатор доставки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа, связанного с доставкой.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Адрес доставки заказа.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Текущий статус доставки.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}