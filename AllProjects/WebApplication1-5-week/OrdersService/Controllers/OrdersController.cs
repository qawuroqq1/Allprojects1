/// <summary>
/// Контроллер для управления заказами через HTTP API.
/// </summary>
namespace OrdersService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OrdersService.DTOs;
    using OrdersService.Services;

    /// <summary>
    /// Предоставляет endpoints для CRUD-операций над заказами.
/// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера заказов.
/// </summary>
/// <param name="orderService">Сервис работы с заказами.</param>
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Возвращает список всех заказов.
/// </summary>
/// <returns>Список заказов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            IEnumerable<OrderDto> result = await this.orderService.GetAllAsync().ConfigureAwait(false);
            return this.Ok(result);
        }

        /// <summary>
        /// Возвращает заказ по идентификатору.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <returns>Заказ или 404.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetByIdAsync(Guid id)
        {
            OrderDto? order = await this.orderService.GetByIdAsync(id).ConfigureAwait(false);

            if (order is null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }

        /// <summary>
        /// Создаёт новый заказ.
/// </summary>
/// <param name="dto">Данные заказа.</param>
/// <returns>Созданный заказ.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] OrderDto dto)
        {
            OrderDto created = await this.orderService.CreateAsync(dto).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetByIdAsync), new { id = created.Id }, created);
        }

        /// <summary>
        /// Обновляет заказ по идентификатору.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <param name="dto">Новые данные заказа.</param>
/// <returns>204 при успехе или 404.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderDto dto)
        {
            bool updated = await this.orderService.UpdateAsync(id, dto).ConfigureAwait(false);

            if (!updated)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        /// Удаляет заказ по идентификатору.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <returns>204 при успехе или 404.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool deleted = await this.orderService.DeleteAsync(id).ConfigureAwait(false);

            if (!deleted)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        /// Возвращает суммарную стоимость всех заказов.
/// </summary>
/// <returns>Сумма стоимости.</returns>
        [HttpGet("total-sum")]
        public async Task<ActionResult<decimal>> GetTotalSumAsync()
        {
            decimal total = await this.orderService.GetTotalSumAsync().ConfigureAwait(false);
            return this.Ok(total);
        }
    }
}