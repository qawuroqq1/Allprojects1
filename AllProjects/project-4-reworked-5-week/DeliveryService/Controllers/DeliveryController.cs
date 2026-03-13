namespace DeliveryService.Controllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// Предоставляет endpoints для чтения доставок.
/// </summary>
[Route("api/delivery")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<DeliveryController> logger;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера доставок.
    /// </summary>
    /// <param name="unitOfWork">Единица работы.</param>
    /// <param name="logger">Логгер.</param>
    public DeliveryController(IUnitOfWork unitOfWork, ILogger<DeliveryController> logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    /// <summary>
    /// Возвращает список всех доставок.
    /// </summary>
    /// <returns>Список доставок.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeliveryOrder>>> GetAllAsync()
    {
        this.logger.LogInformation("GET /api/delivery called");

        var deliveries = await this.unitOfWork.DeliveryOrders.GetAllAsync();

        this.logger.LogInformation("GET /api/delivery returned {Count} deliveries", deliveries.Count);

        return this.Ok(deliveries);
    }

    /// <summary>
    /// Возвращает доставку по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор доставки.</param>
    /// <returns>Объект доставки, если найден, или null, если доставка не найдена.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeliveryOrder?>> GetByIdAsync(Guid id)
    {
        this.logger.LogInformation("GET /api/delivery/{Id} called", id);

        var delivery = await this.unitOfWork.DeliveryOrders.GetByIdAsync(id);

        return delivery;
    }
}