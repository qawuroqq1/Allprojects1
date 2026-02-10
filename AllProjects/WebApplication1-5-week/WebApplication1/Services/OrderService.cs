namespace WebApplication1.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Models;

    public class OrderService
    {
        private readonly AppDbContext context;

        public OrderService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllAsync(OrderStatus? status)
        {
            var query = this.context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await this.context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<bool> UpdateAsync(Order updatedOrder)
        {
            var exists = await this.context.Orders.AnyAsync(o => o.Id == updatedOrder.Id);
            if (!exists)
            {
                return false;
            }

            this.context.Entry(updatedOrder).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await this.context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            this.context.Orders.Remove(order);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSumAsync(OrderStatus? status)
        {
            var query = this.context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return await query.SumAsync(o => o.Price);
        }
    }
}       