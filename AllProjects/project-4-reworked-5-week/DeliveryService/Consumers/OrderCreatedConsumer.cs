namespace DeliveryService.Consumers
{
    using System;
    using System.Threading.Tasks;
    using DeliveryService.Models;
    using DeliveryService.Repositories;
    using MassTransit;

    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderCreatedConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.DeliveryOrders.AddAsync(deliveryOrder);
            await _unitOfWork.CompleteAsync();
        }
    }
}