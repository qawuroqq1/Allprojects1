/// <summary>
/// </summary>
namespace OrdersService.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using OrdersService.Models;

    public sealed class OrderRepository : IOrderRepository
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

        public async Task<OrderEntity?> GetByIdAsync(Guid id)
        {
            return await this.context.Orders.FindAsync(id).ConfigureAwait(false);
        }

        public async Task AddAsync(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            await this.context.Orders.AddAsync(order).ConfigureAwait(false);
        }

        public void Update(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            this.context.Orders.Update(order);
        }

        public void Remove(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            this.context.Orders.Remove(order);
        }

        public async Task<decimal> GetTotalSumAsync()
        {
            return await this.context.Orders.SumAsync(x => x.Price).ConfigureAwait(false);
        }
    }
}