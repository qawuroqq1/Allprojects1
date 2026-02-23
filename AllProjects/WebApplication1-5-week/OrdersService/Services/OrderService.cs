/// <summary>
/// Реализация сервиса бизнес-логики для работы с заказами.
/// </summary>
namespace OrdersService.Services
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    /// <summary>
    /// Реализует операции бизнес-логики заказов через Unit of Work.
/// </summary>
    public sealed class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса заказов.
/// </summary>
/// <param name="unitOfWork">Unit of Work для доступа к данным.</param>
/// <param name="mapper">Компонент маппинга.</param>
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <summary>
        /// Возвращает список всех заказов.
/// </summary>
/// <returns>Коллекция заказов.</returns>
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            IEnumerable<OrderEntity> entities = await this.unitOfWork.Orders.GetAllAsync().ConfigureAwait(false);
            return this.mapper.Map<IEnumerable<OrderDto>>(entities);
        }

        /// <summary>
        /// Возвращает заказ по идентификатору.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <returns>Заказ или null.</returns>
        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            OrderEntity? entity = await this.unitOfWork.Orders.GetByIdAsync(id).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            return this.mapper.Map<OrderDto>(entity);
        }

        /// <summary>
        /// Создаёт новый заказ.
/// </summary>
/// <param name="dto">Данные заказа.</param>
/// <returns>Созданный заказ.</returns>
        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            OrderEntity entity = this.mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();

            await this.unitOfWork.Orders.AddAsync(entity).ConfigureAwait(false);
            await this.unitOfWork.CompleteAsync().ConfigureAwait(false);

            return this.mapper.Map<OrderDto>(entity);
        }

        /// <summary>
        /// Обновляет существующий заказ.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <param name="dto">Новые данные заказа.</param>
/// <returns>True, если заказ обновлён.</returns>
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

        /// <summary>
        /// Удаляет заказ.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <returns>True, если заказ удалён.</returns>
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

        /// <summary>
        /// Возвращает суммарную стоимость всех заказов.
/// </summary>
/// <returns>Сумма стоимости.</returns>
        public async Task<decimal> GetTotalSumAsync()
        {
            return await this.unitOfWork.Orders.GetTotalSumAsync().ConfigureAwait(false);
        }
    }
}