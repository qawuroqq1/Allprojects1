/// <summary>
/// </summary>
namespace OrdersService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OrdersService.DTOs;
    using OrdersService.Services;

    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            IEnumerable<OrderDto> result = await this.orderService.GetAllAsync().ConfigureAwait(false);
            return this.Ok(result);
        }

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

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] OrderDto dto)
        {
            OrderDto created = await this.orderService.CreateAsync(dto).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetByIdAsync), new { id = created.Id }, created);
        }

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

        [HttpGet("total-sum")]
        public async Task<ActionResult<decimal>> GetTotalSumAsync()
        {
            decimal total = await this.orderService.GetTotalSumAsync().ConfigureAwait(false);
            return this.Ok(total);
        }
    }
}