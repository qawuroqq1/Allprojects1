namespace DeliveryService.Consumers
{
    using System;
    using System.Threading.Tasks;
    using DeliveryService.Models;
    using DeliveryService.Repositories;
    using MassTransit;

    // (по ревью) УБРАЛИ только XML summary над классом
    public sealed class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderCreatedConsumer(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var deliveryOrder = new DeliveryOrder
            {
                Id = Guid.NewGuid(),
                OrderId = context.Message.OrderId,
                Address = context.Message.Address,
                Status = "Pending",
            };

            await this.unitOfWork.DeliveryOrders.AddAsync(deliveryOrder);
            await this.unitOfWork.CompleteAsync();
        }
    }
}