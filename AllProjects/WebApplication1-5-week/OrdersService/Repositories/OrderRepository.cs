using Microsoft.EntityFrameworkCore;
using OrdersService.Models;

namespace OrdersService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext context;

    public OrderRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        return await this.context.Orders.ToListAsync().ConfigureAwait(false);
    }

    public async Task<OrderEntity> GetByIdAsync(Guid id)
    {
        return await this.context.Orders.FindAsync(id).ConfigureAwait(false);
    }

    public async Task AddAsync(OrderEntity order)
    {
        await this.context.Orders.AddAsync(order).ConfigureAwait(false);
    }

    public async Task SaveChangesAsync()
    {
        await this.context.SaveChangesAsync().ConfigureAwait(false);
    }
}