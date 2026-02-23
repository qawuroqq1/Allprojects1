namespace OrdersService.Services
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    /// <summary>
    /// Сервис бизнес-логики для работы с заказами.
    /// </summary>
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
            var entities = await this.unitOfWork.Orders.GetAllAsync();
            return this.mapper.Map<IEnumerable<OrderDto>>(entities);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var entity = await this.unitOfWork.Orders.GetByIdAsync(id);

            if (entity is null)
            {
                return null;
            }

            return this.mapper.Map<OrderDto>(entity);
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = this.mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();

            await this.unitOfWork.Orders.AddAsync(entity);
            await this.unitOfWork.CompleteAsync();

            return this.mapper.Map<OrderDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, OrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var existing = await this.unitOfWork.Orders.GetByIdAsync(id);

            if (existing is null)
            {
                return false;
            }

            this.mapper.Map(dto, existing);
            this.unitOfWork.Orders.Update(existing);

            await this.unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await this.unitOfWork.Orders.GetByIdAsync(id);

            if (existing is null)
            {
                return false;
            }

            this.unitOfWork.Orders.Remove(existing);
            await this.unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<decimal> GetTotalSumAsync()
        {
            return await this.unitOfWork.Orders.GetTotalSumAsync();
        }
    }
}