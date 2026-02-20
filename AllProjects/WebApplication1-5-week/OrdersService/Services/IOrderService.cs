/// <summary>
/// </summary>
namespace OrdersService.Services
{
    using OrdersService.DTOs;

    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(Guid id);

        Task<OrderDto> CreateAsync(OrderDto dto);

        Task<bool> UpdateAsync(Guid id, OrderDto dto);

        Task<bool> DeleteAsync(Guid id);

        Task<decimal> GetTotalSumAsync();
    }
}