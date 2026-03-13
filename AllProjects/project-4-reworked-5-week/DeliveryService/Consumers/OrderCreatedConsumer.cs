using System;
using System.Threading.Tasks;
using MassTransit;
using DeliveryService.Models;
using OrdersService.Contracts;
using DeliveryService.Repositories;

namespace DeliveryService.Consumers;

/// <summary>
/// Обработчик события создания заказа.
/// Создаёт запись доставки при получении сообщения из RabbitMQ.
/// </summary>
public class OrderCreatedConsumer : IConsumer<IOrderCreated>
{
    private readonly IUnitOfWork unitOfWork;

    public OrderCreatedConsumer(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<IOrderCreated> context)
    {
        var delivery = new DeliveryOrder
        {
            Id = Guid.NewGuid(),
            OrderId = context.Message.OrderId,
            Address = "Auto-generated address",
            Status = "Created"
        };

        await this.unitOfWork.DeliveryOrders.AddAsync(delivery);
        await this.unitOfWork.CompleteAsync();
    }
}