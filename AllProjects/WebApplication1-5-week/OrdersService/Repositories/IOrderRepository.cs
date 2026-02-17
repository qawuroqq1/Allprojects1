using OrdersService.Models;

namespace OrdersService.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderEntity>> GetAllAsync();
    Task<OrderEntity> GetByIdAsync(Guid id);
    Task AddAsync(OrderEntity order);
    Task SaveChangesAsync();
}
