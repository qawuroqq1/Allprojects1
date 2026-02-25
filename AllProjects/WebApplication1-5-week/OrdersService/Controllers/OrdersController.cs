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
    /// <summary>
    /// Возвращает список всех заказов.
    /// </summary>
    /// <returns>Список заказов.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var result = await orderService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto?>> GetByIdAsync(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        return Ok(order);
    }

    /// <summary>
    /// Создаёт новый заказ.
    /// </summary>
    /// <param name="dto">Данные заказа.</param>
    /// <returns>Созданный заказ.</returns>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] OrderDto dto)
    {
        var result = await orderService.CreateAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>True/False.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderDto dto)
    {
        var updated = await orderService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// Удаляет заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True/False.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await orderService.DeleteAsync(id);
        return Ok(deleted);
    }

    /// <summary>
    /// Возвращает суммарную стоимость всех заказов.
    /// </summary>
    /// <returns>Сумма стоимости.</returns>
    [HttpGet("total-sum")]
    public async Task<ActionResult<decimal>> GetTotalSumAsync()
    {
        var total = await orderService.GetTotalSumAsync();
        return Ok(total);
    }
}