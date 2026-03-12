﻿namespace DeliveryService.Consumers;

using MassTransit;
using Microsoft.Extensions.Logging;
using Models;
using OrdersService.Contracts;
using Repositories;

/// <summary>
/// Обработчик события создания заказа.
/// Создаёт запись доставки при получении сообщения из RabbitMQ.
/// </summary>
public class OrderCreatedConsumer : IConsumer<IOrderCreated>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<OrderCreatedConsumer> logger;

    /// <summary>
    /// Инициализирует новый экземпляр consumer.
    /// </summary>
    /// <param name="unitOfWork">Единица работы для взаимодействия с БД.</param>
    /// <param name="logger">Логгер.</param>
    public OrderCreatedConsumer(IUnitOfWork unitOfWork, ILogger<OrderCreatedConsumer> logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    /// <summary>
    /// Обрабатывает событие создания заказа.
    /// </summary>
    /// <param name="context">Контекст сообщения.</param>
    public async Task Consume(ConsumeContext<IOrderCreated> context)
    {
        try
        {
            this.logger.LogInformation("Received OrderCreated event. OrderId: {OrderId}", context.Message.OrderId);

            var delivery = new DeliveryOrder
            {
                Id = Guid.NewGuid(),
                OrderId = context.Message.OrderId,
                Address = "Auto-generated address",
                Status = "Created"
            };

            this.logger.LogInformation("Adding delivery to database. DeliveryId: {DeliveryId}, OrderId: {OrderId}",
                delivery.Id,
                delivery.OrderId);

            await this.unitOfWork.DeliveryOrders.AddAsync(delivery);

            this.logger.LogInformation("Calling SaveChangesAsync for OrderId: {OrderId}", context.Message.OrderId);

            var result = await this.unitOfWork.CompleteAsync();

            this.logger.LogInformation("Delivery saved successfully. Rows affected: {RowsAffected}, OrderId: {OrderId}",
                result,
                context.Message.OrderId);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error while processing OrderCreated event. OrderId: {OrderId}",
                context.Message.OrderId);
            throw;
        }
    }
}