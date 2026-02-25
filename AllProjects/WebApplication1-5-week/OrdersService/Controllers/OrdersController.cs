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
        public async Task<IActionResult> GetAll()
        {
            var orders = await this.orderService.GetAllAsync().ConfigureAwait(false);
            return this.Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await this.orderService.GetByIdAsync(id).ConfigureAwait(false);

            if (order is null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
        {
            var created = await this.orderService.CreateAsync(dto).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderDto dto)
        {
            var updated = await this.orderService.UpdateAsync(id, dto).ConfigureAwait(false);

            if (!updated)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await this.orderService.DeleteAsync(id).ConfigureAwait(false);

            if (!deleted)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpGet("total-sum")]
        public async Task<IActionResult> GetTotalSum()
        {
            var total = await this.orderService.GetTotalSumAsync().ConfigureAwait(false);
            return this.Ok(total);
        }
    }
}