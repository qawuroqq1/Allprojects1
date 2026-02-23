namespace OrdersService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OrdersService.DTOs;
    using OrdersService.Services;

    /// <summary>
    /// Контроллер для управления заказами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Возвращает список всех заказов.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            var result = await this.orderService.GetAllAsync();
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
            var order = await this.orderService.GetByIdAsync(id);

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
            var created = await this.orderService.CreateAsync(dto);
            return this.CreatedAtAction(nameof(this.GetByIdAsync), new { id = created.Id }, created);
        }

        /// <summary>
        /// Обновляет заказ.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <param name="dto">Новые данные заказа.</param>
        /// <returns>204 или 404.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderDto dto)
        {
            var updated = await this.orderService.UpdateAsync(id, dto);

            if (!updated)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>204 или 404.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await this.orderService.DeleteAsync(id);

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
            var total = await this.orderService.GetTotalSumAsync();
            return this.Ok(total);
        }
    }
}