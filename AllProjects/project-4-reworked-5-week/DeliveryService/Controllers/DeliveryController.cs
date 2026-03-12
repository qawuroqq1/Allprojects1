﻿namespace DeliveryService.Controllers
{
    using DeliveryService.Repositories;
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
        public async Task<IActionResult> GetAllAsync()
        {
            this.logger.LogInformation("GET /api/delivery called");

            var deliveries = await this.unitOfWork.DeliveryOrders.GetAllAsync();

            this.logger.LogInformation("GET /api/delivery returned {Count} deliveries", deliveries.Count);

            return this.Ok(deliveries);
        }

        /// <summary>
        /// Возвращает доставку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор доставки.</param>
        /// <returns>Доставка или 404.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            this.logger.LogInformation("GET /api/delivery/{Id} called", id);

            var delivery = await this.unitOfWork.DeliveryOrders.GetByIdAsync(id);

            if (delivery is null)
            {
                this.logger.LogWarning("Delivery not found. Id: {Id}", id);
                return this.NotFound();
            }

            this.logger.LogInformation("Delivery found. Id: {Id}", id);
            return this.Ok(delivery);
        }
    }
}