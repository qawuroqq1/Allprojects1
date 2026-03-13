using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.Services;
using DeliveryService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers;

/// <summary>
/// Предоставляет endpoints для чтения доставок.
/// </summary>
[Route("api/delivery")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService deliveryService;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера доставок.
    /// </summary>
    /// <param name="deliveryService">Сервис доставок.</param>
    public DeliveryController(IDeliveryService deliveryService)
    {
        this.deliveryService = deliveryService;
    }

    /// <summary>
    /// Возвращает список всех доставок.
    /// </summary>
    /// <returns>Список доставок.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeliveryViewModel>>> GetAllAsync()
    {
        var deliveries = await this.deliveryService.GetAllAsync();
        return this.Ok(deliveries);
    }

    /// <summary>
    /// Возвращает доставку по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор доставки.</param>
    /// <returns>Объект доставки, если найден.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeliveryViewModel>> GetByIdAsync(Guid id)
    {
        var delivery = await this.deliveryService.GetByIdAsync(id);

        if (delivery is null)
        {
            return this.NotFound();
        }

        return this.Ok(delivery);
    }
}