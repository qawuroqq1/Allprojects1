using AutoMapper;
using OrdersService.DTOs;
using OrdersService.Models;
using OrdersService.Repositories;

namespace OrdersService.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<OrderDto> CreateAsync(OrderDto dto)
    {
        var entity = this.mapper.Map<OrderEntity>(dto);
        await this.unitOfWork.Orders.AddAsync(entity).ConfigureAwait(false);
        await this.unitOfWork.CompleteAsync().ConfigureAwait(false);
        return this.mapper.Map<OrderDto>(entity);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var entities = await this.unitOfWork.Orders.GetAllAsync().ConfigureAwait(false);
        return this.mapper.Map<IEnumerable<OrderDto>>(entities);
    }
}