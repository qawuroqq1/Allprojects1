using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.ViewModels;

namespace DeliveryService.Services;

/// <summary>
/// Определяет бизнес-логику для работы с доставками.
/// </summary>
public interface IDeliveryService
{
    /// <summary>
    /// Возвращает список всех доставок.
    /// </summary>
    /// <returns>Коллекция доставок в виде модели представления.</returns>
    Task<IEnumerable<DeliveryViewModel>> GetAllAsync();

    /// <summary>
    /// Возвращает доставку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор доставки.</param>
    /// <returns>Модель представления доставки или null.</returns>
    Task<DeliveryViewModel?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создаёт доставку на основе созданного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    Task CreateFromOrderAsync(Guid orderId);
}