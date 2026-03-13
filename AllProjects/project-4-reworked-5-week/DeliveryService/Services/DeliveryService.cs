using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeliveryService.Models;
using DeliveryService.Repositories;
using DeliveryService.ViewModels;

namespace DeliveryService.Services;

/// <summary>
/// Реализует бизнес-логику для работы с доставками.
/// </summary>
public class DeliveryService : IDeliveryService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса доставок.
    /// </summary>
    /// <param name="unitOfWork">Единица работы.</param>
    /// <param name="mapper">Маппер.</param>
    public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    /// <summary>
    /// Возвращает список всех доставок.
    /// </summary>
    /// <returns>Коллекция доставок в виде модели представления.</returns>
    public async Task<IEnumerable<DeliveryViewModel>> GetAllAsync()
    {
        var deliveries = await this.unitOfWork.DeliveryOrders.GetAllAsync();
        return this.mapper.Map<IEnumerable<DeliveryViewModel>>(deliveries);
    }

    /// <summary>
    /// Возвращает доставку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор доставки.</param>
    /// <returns>Модель представления доставки или null.</returns>
    public async Task<DeliveryViewModel?> GetByIdAsync(Guid id)
    {
        var delivery = await this.unitOfWork.DeliveryOrders.GetByIdAsync(id);

        if (delivery is null)
        {
            return null;
        }

        return this.mapper.Map<DeliveryViewModel>(delivery);
    }

    /// <summary>
    /// Создаёт доставку на основе созданного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    public async Task CreateFromOrderAsync(Guid orderId)
    {
        var delivery = new DeliveryOrder
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Address = "Auto-generated address",
            Status = "Created"
        };

        await this.unitOfWork.DeliveryOrders.AddAsync(delivery);
        await this.unitOfWork.CompleteAsync();
    }
}