/// <summary>
/// Consumer события создания заказа. Создаёт запись доставки на основе сообщения.
/// </summary>
namespace DeliveryService.Consumers
{
    using DeliveryService.Models;
    using DeliveryService.Repositories;
    using MassTransit;

    /// <summary>
    /// Обрабатывает событие IOrderCreated и создаёт DeliveryOrder.
    /// </summary>
    public sealed class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр consumer.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для сохранения данных.</param>
        public OrderCreatedConsumer(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Обрабатывает входящее сообщение о создании заказа.
        /// </summary>
        /// <param name="context">Контекст сообщения.</param>
        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            DeliveryOrder deliveryOrder = new DeliveryOrder
            {
                Id = Guid.NewGuid(),
                OrderId = context.Message.OrderId,
                Address = context.Message.Address,
                Status = "Pending",
            };

            await this.unitOfWork.DeliveryOrders.AddAsync(deliveryOrder).ConfigureAwait(false);
            await this.unitOfWork.CompleteAsync().ConfigureAwait(false);
        }
    }
}