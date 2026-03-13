namespace DeliveryService.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Реализация репозитория доставок.
/// </summary>
public class DeliveryRepository : IDeliveryRepository
{
    private readonly DeliveryDbContext context;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория доставок.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public DeliveryRepository(DeliveryDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Добавляет новую запись доставки.
    /// </summary>
    /// <param name="deliveryOrder">Сущность доставки.</param>
    public async Task AddAsync(DeliveryOrder deliveryOrder)
    {
        ArgumentNullException.ThrowIfNull(deliveryOrder);

        await this.context.DeliveryOrders.AddAsync(deliveryOrder);
    }

    /// <summary>
    /// Возвращает список всех доставок.
    /// </summary>
    /// <returns>Коллекция доставок.</returns>
    public async Task<List<DeliveryOrder>> GetAllAsync()
    {
        return await this.context.DeliveryOrders.ToListAsync();
    }

    /// <summary>
    /// Возвращает доставку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор доставки.</param>
    /// <returns>Доставка или null.</returns>
    public async Task<DeliveryOrder?> GetByIdAsync(Guid id)
    {
        return await this.context.DeliveryOrders.FirstOrDefaultAsync(x => x.Id == id);
    }
}