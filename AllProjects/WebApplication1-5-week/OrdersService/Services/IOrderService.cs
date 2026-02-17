using OrdersService.DTOs;

namespace OrdersService.Services;

public interface IOrderService
{
    Task<OrderDto> CreateAsync(OrderDto dto);
    Task<IEnumerable<OrderDto>> GetAllAsync();
}