using Microsoft.AspNetCore.Mvc;
using OrdersService.DTOs;
using OrdersService.Services;

namespace OrdersService.Controllers;

/// <summary>
/// Предоставляет endpoints для CRUD-операций над заказами.
/// </summary>
/// <param name="orderService">Сервис работы с заказами.</param>
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var result = await orderService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto?>> GetByIdAsync(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] OrderDto dto)
    {
        var result = await orderService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderDto dto)
    {
        var updated = await orderService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await orderService.DeleteAsync(id);
        return Ok(deleted);
    }

    [HttpGet("total-sum")]
    public async Task<ActionResult<decimal>> GetTotalSumAsync()
    {
        var total = await orderService.GetTotalSumAsync();
        return Ok(total);
    }
}