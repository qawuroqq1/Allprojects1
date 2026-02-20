/// <summary>
/// </summary>
namespace OrdersService.Repositories
{
    using OrdersService.Models;

    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetAllAsync();

        Task<OrderEntity?> GetByIdAsync(Guid id);

        Task AddAsync(OrderEntity order);

        void Update(OrderEntity order);

        void Remove(OrderEntity order);

        Task<decimal> GetTotalSumAsync();
    }
}