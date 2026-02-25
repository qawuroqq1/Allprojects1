using OrdersService.DTOs;

namespace OrdersService.Services;

/// <summary>
/// Контракт сервиса бизнес-логики для работы с заказами.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    Task<IEnumerable<OrderDto>> GetAllAsync();

    /// <summary>
    /// Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null.</returns>
    Task<OrderDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создаёт новый заказ.
    /// </summary>
    /// <param name="dto">Данные заказа.</param>
    /// <returns>Созданный заказ.</returns>
    Task<OrderDto> CreateAsync(OrderDto dto);

    /// <summary>
    /// Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>True, если заказ обновлён.</returns>
    Task<bool> UpdateAsync(Guid id, OrderDto dto);

    /// <summary>
    /// Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True, если заказ удалён.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Возвращает суммарную стоимость всех заказов.
    /// </summary>
    /// <returns>Сумма стоимости.</returns>
    Task<decimal> GetTotalSumAsync();
}