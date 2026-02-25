using Microsoft.AspNetCore.Mvc;
using OrdersService.DTOs;
using OrdersService.Services;

namespace OrdersService.Controllers;

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
        var result = await this.orderService.GetAllAsync();
        return this.Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto?>> GetByIdAsync(Guid id)
    {
        var order = await this.orderService.GetByIdAsync(id);

        if (order is null)
        {
            return this.NotFound();
        }

        return this.Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] OrderDto dto)
    {
        var created = await this.orderService.CreateAsync(dto);
        return this.CreatedAtAction(nameof(this.GetByIdAsync), new { id = created.Id }, created);
    }

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

    [HttpGet("total-sum")]
    public async Task<ActionResult<decimal>> GetTotalSumAsync()
    {
        var total = await this.orderService.GetTotalSumAsync();
        return this.Ok(total);
    }
}