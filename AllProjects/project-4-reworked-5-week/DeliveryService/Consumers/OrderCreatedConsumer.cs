using DeliveryService.Models;
using MassTransit;

namespace DeliveryService.Consumers
{
    public interface IOrderCreated
    {
        Guid OrderId { get; }
        string Address { get; }
    }

    public abstract class OrderCreatedConsumer(DeliveryDbContext context) : IConsumer<IOrderCreated>
    {
        public async Task Consume(ConsumeContext<IOrderCreated> context1)
        {
            var deliveryOrder = new DeliveryOrder
            {
                Id = Guid.NewGuid(),
                OrderId = context1.Message.OrderId,
                Address = context1.Message.Address,
                Status = "Pending"
            };

            context.DeliveryOrders.Add(deliveryOrder);
            await context.SaveChangesAsync();
        }
    }
}