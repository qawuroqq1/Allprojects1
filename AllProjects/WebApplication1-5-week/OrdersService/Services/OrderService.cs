/// <summary>
/// </summary>
namespace OrdersService.Services
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    public sealed class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            IEnumerable<OrderEntity> entities = await this.unitOfWork.Orders.GetAllAsync().ConfigureAwait(false);
            return this.mapper.Map<IEnumerable<OrderDto>>(entities);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            OrderEntity? entity = await this.unitOfWork.Orders.GetByIdAsync(id).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            return this.mapper.Map<OrderDto>(entity);
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            OrderEntity entity = this.mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();

            await this.unitOfWork.Orders.AddAsync(entity).ConfigureAwait(false);
            await this.unitOfWork.CompleteAsync().ConfigureAwait(false);

            return this.mapper.Map<OrderDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, OrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            OrderEntity? existing = await this.unitOfWork.Orders.GetByIdAsync(id).ConfigureAwait(false);

            if (existing is null)
            {
                return false;
            }

            this.mapper.Map(dto, existing);
            this.unitOfWork.Orders.Update(existing);

            await this.unitOfWork.CompleteAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            OrderEntity? existing = await this.unitOfWork.Orders.GetByIdAsync(id).ConfigureAwait(false);

            if (existing is null)
            {
                return false;
            }

            this.unitOfWork.Orders.Remove(existing);
            await this.unitOfWork.CompleteAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<decimal> GetTotalSumAsync()
        {
            return await this.unitOfWork.Orders.GetTotalSumAsync().ConfigureAwait(false);
        }
    }
}