namespace DeliveryService.Consumers;

using DeliveryService.Contracts;
using DeliveryService.Models;
using DeliveryService.Repositories;
using MassTransit;

/// <summary>
/// Обработчик события создания заказа.
/// Создаёт запись доставки при получении сообщения из RabbitMQ.
/// </summary>
public class OrderCreatedConsumer : IConsumer<IOrderCreated>
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Инициализирует новый экземпляр consumer.
    /// </summary>
    /// <param name="unitOfWork">Единица работы для взаимодействия с БД.</param>
    public OrderCreatedConsumer(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Обрабатывает событие создания заказа.
    /// </summary>
    /// <param name="context">Контекст сообщения.</param>
    public async Task Consume(ConsumeContext<IOrderCreated> context)
    {
        var delivery = new DeliveryOrder
        {
            Id = Guid.NewGuid(),
            OrderId = context.Message.OrderId,
            Address = "Auto-generated address",
            Status = "Created"
        };

        await unitOfWork.DeliveryOrders.AddAsync(delivery);
        await unitOfWork.CompleteAsync();
    }
}