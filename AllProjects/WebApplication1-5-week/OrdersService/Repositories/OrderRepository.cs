namespace OrdersService.Repositories;

using Microsoft.EntityFrameworkCore;
using Models;

/// <summary>
/// Репозиторий заказов на основе Entity Framework Core.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public OrderRepository(OrderDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Возвращает все заказы.
    /// </summary>
    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    /// <summary>
    /// Возвращает заказ по идентификатору.
    /// </summary>
    public async Task<OrderEntity?> GetByIdAsync(Guid id)
    {
        return await context.Orders.FindAsync(id);
    }

    /// <summary>
    /// Добавляет заказ.
    /// </summary>
    public async Task AddAsync(OrderEntity order)
    {
        await context.Orders.AddAsync(order);
    }

    /// <summary>
    /// Обновляет заказ.
    /// </summary>
    public void Update(OrderEntity order)
    {
        context.Orders.Update(order);
    }

    /// <summary>
    /// Удаляет заказ.
    /// </summary>
    public void Remove(OrderEntity order)
    {
        context.Orders.Remove(order);
    }

    /// <summary>
    /// Возвращает общую сумму заказов.
    /// </summary>
    public async Task<decimal> GetTotalSumAsync()
    {
        return await context.Orders.SumAsync(x => x.Price);
    }
}