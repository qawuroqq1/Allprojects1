/// <summary>
/// Контроллер для просмотра данных доставок через HTTP API.
/// </summary>
namespace DeliveryService.Controllers
{
    using DeliveryService.Repositories;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Предоставляет endpoints для чтения доставок.
    /// </summary>
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера доставок.
        /// </summary>
        /// <param name="unitOfWork">Единица работы.</param>
        public DeliveryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Возвращает список всех доставок.
        /// </summary>
        /// <returns>Список доставок.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var deliveries = await _unitOfWork.DeliveryOrders.GetAllAsync();
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
            var delivery = await _unitOfWork.DeliveryOrders.GetByIdAsync(id);

            if (delivery is null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }
    }
}