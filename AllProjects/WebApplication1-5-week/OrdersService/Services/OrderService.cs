namespace OrdersService.Services
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    /// <summary>
    /// Реализует бизнес-логику работы с заказами.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса заказов.
        /// </summary>
        /// <param name="unitOfWork">Единица работы.</param>
        /// <param name="mapper">AutoMapper.</param>
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <summary>
        /// Возвращает все заказы.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var entities = await unitOfWork.Orders.GetAllAsync();
            return mapper.Map<IEnumerable<OrderDto>>(entities);
        }

        /// <summary>
        /// Возвращает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>DTO заказа или null.</returns>
        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var entity = await unitOfWork.Orders.GetByIdAsync(id);

            if (entity is null)
            {
                return null;
            }

            return mapper.Map<OrderDto>(entity);
        }

        /// <summary>
        /// Создаёт новый заказ.
        /// </summary>
        /// <param name="dto">Данные заказа.</param>
        /// <returns>Созданный заказ.</returns>
        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var entity = mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();

            await unitOfWork.Orders.AddAsync(entity);
            await unitOfWork.CompleteAsync();

            return mapper.Map<OrderDto>(entity);
        }

        /// <summary>
        /// Обновляет заказ.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <param name="dto">Новые данные.</param>
        /// <returns>True если обновлено успешно.</returns>
        public async Task<bool> UpdateAsync(Guid id, OrderDto dto)
        {
            var existing = await unitOfWork.Orders.GetByIdAsync(id);

            if (existing is null)
            {
                return false;
            }

            mapper.Map(dto, existing);
            unitOfWork.Orders.Update(existing);

            await unitOfWork.CompleteAsync();
            return true;
        }

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>True если удалён.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await unitOfWork.Orders.GetByIdAsync(id);

            if (existing is null)
            {
                return false;
            }

            unitOfWork.Orders.Remove(existing);
            await unitOfWork.CompleteAsync();
            return true;
        }

        /// <summary>
        /// Возвращает суммарную стоимость всех заказов.
        /// </summary>
        /// <returns>Общая сумма.</returns>
        public async Task<decimal> GetTotalSumAsync()
        {
            return await unitOfWork.Orders.GetTotalSumAsync();
        }
    }
}