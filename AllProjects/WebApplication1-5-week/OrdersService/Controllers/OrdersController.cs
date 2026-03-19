using OrdersService.Domain.DTOs;

namespace OrdersService.Controllers;

using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var result = await this.orderService.GetAllAsync();
        return this.Ok(result);
    }

    /// <summary>
    /// Возвращает заказ по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Объект заказа, если найден, или null, если заказ не найден.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto?>> GetById(Guid id)
    {
        var result = await this.orderService.GetByIdAsync(id);

        return result;
    }

    /// <summary>
    /// Создаёт новый заказ.
    /// </summary>
    /// <param name="dto">Данные заказа.</param>
    /// <returns>Созданный заказ.</returns>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto dto)
    {
        var createdOrder = await this.orderService.CreateAsync(dto);

        return this.CreatedAtAction(nameof(this.GetById), new { id = createdOrder.Id }, createdOrder);
    }

    /// <summary>
    /// Обновляет заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Результат обновления.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderDto dto)
    {
        var updated = await this.orderService.UpdateAsync(id, dto);

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
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await this.orderService.DeleteAsync(id);

        if (!deleted)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }

    /// <summary>
    /// Возвращает общую сумму всех заказов.
    /// </summary>
    /// <returns>Общая сумма заказов.</returns>
    [HttpGet("total-sum")]
    public async Task<ActionResult<decimal>> GetTotalSum()
    {
        var totalSum = await this.orderService.GetTotalSumAsync();

        return this.Ok(totalSum);
    }
}