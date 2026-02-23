/// <summary>
/// Контроллер для просмотра данных доставок через HTTP API.
/// </summary>
namespace DeliveryService.Controllers
{
    using DeliveryService.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Предоставляет endpoints для чтения доставок.
    /// </summary>
    [Route("api/delivery")]
    [ApiController]
    public sealed class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext context;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера доставок.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DeliveryController(DeliveryDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Возвращает список всех доставок.
        /// </summary>
        /// <returns>Список доставок.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<DeliveryOrder> deliveries = await this.context.DeliveryOrders.ToListAsync().ConfigureAwait(false);
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
            DeliveryOrder? delivery = await this.context.DeliveryOrders.FindAsync(id).ConfigureAwait(false);

            if (delivery is null)
            {
                return this.NotFound();
            }

            return this.Ok(delivery);
        }
    }
}