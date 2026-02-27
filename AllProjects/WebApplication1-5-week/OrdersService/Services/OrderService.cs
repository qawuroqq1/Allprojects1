namespace OrdersService.Services
{
    using AutoMapper;
    using MassTransit;
    using OrdersService.Contracts;
    using OrdersService.DTOs;
    using OrdersService.Models;
    using OrdersService.Repositories;

    /// <summary>
    /// Реализует бизнес-логику работы с заказами.
    /// Отвечает за управление заказами и публикацию событий в шину сообщений.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// Инициализирует новый экземпляр сервиса заказов.
        /// </summary>
        /// <param name="unitOfWork">Единица работы для управления транзакциями.</param>
        /// <param name="mapper">AutoMapper для преобразования DTO и сущностей.</param>
        /// <param name="publishEndpoint">MassTransit endpoint для публикации событий.</param>
        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
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
        /// <returns>DTO заказа или null, если не найден.</returns>
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
        /// Создаёт новый заказ и публикует событие о его создании.
        /// </summary>
        /// <param name="dto">Данные заказа.</param>
        /// <returns>Созданный заказ.</returns>
        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var entity = mapper.Map<OrderEntity>(dto);
            entity.Id = Guid.NewGuid();

            await unitOfWork.Orders.AddAsync(entity);
            await unitOfWork.CompleteAsync();

            /// <summary>
            /// Публикует событие о создании заказа в RabbitMQ.
            /// </summary>
            await publishEndpoint.Publish<IOrderCreated>(new
            {
                OrderId = entity.Id
            });

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
        /// <returns>Общая сумма заказов.</returns>
        public async Task<decimal> GetTotalSumAsync()
        {
            return await unitOfWork.Orders.GetTotalSumAsync();
        }
    }
}