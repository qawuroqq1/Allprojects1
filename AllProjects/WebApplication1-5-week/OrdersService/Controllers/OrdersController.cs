namespace OrdersService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OrdersService.DTOs;
    using OrdersService.Services;

    /// <summary>
    /// Контроллер для работы с заказами.
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
        /// Возвращает список заказов.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await orderService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Возвращает заказ по идентификатору.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await orderService.GetByIdAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Создаёт новый заказ.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            var created = await orderService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        /// <summary>
        /// Обновляет заказ.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, OrderDto dto)
        {
            var updated = await orderService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await orderService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}