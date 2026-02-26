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
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера доставок.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DeliveryController(DeliveryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех доставок.
        /// </summary>
        /// <returns>Список доставок.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var deliveries = await _context.DeliveryOrders.ToListAsync();
            return Ok(deliveries);
        }

        /// <summary>
        /// Возвращает доставку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор доставки.</param>
        /// <returns>Доставка или 404.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var delivery = await _context.DeliveryOrders.FindAsync(id);

            if (delivery is null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }
    }
}