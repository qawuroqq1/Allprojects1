namespace OrdersService.Services
{
    using AutoMapper;
    using MassTransit;
    using OrdersService.Contracts;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var entities = await unitOfWork.Orders.GetAllAsync();
            return mapper.Map<IEnumerable<OrderDto>>(entities);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var entity = await unitOfWork.Orders.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<OrderDto>(entity);
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var entity = mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();
            entity.OrderDate = DateTime.UtcNow;

            await unitOfWork.Orders.AddAsync(entity);
            await unitOfWork.CompleteAsync();

            await publishEndpoint.Publish<IOrderCreated>(new
            {
                OrderId = entity.Id
            });

            return mapper.Map<OrderDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, OrderDto dto)
        {
            var existing = await unitOfWork.Orders.GetByIdAsync(id);
            if (existing is null) return false;

            mapper.Map(dto, existing);
            unitOfWork.Orders.Update(existing);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await unitOfWork.Orders.GetByIdAsync(id);
            if (existing is null) return false;

            unitOfWork.Orders.Remove(existing);
            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<decimal> GetTotalSumAsync()
        {
            return await unitOfWork.Orders.GetTotalSumAsync();
        }
    }
}