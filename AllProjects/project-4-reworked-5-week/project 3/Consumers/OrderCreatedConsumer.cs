using MassTransit;
using project_3.Models;

namespace project_3.Consumers
{
    public interface IOrderCreated
    {
        Guid OrderId { get; }
        string Address { get; }
    }

    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        private readonly DeliveryDbContext _context;

        public OrderCreatedConsumer(DeliveryDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var deliveryOrder = new DeliveryOrder
            {
                Id = Guid.NewGuid(),
                OrderId = context.Message.OrderId,
                Address = context.Message.Address,
                Status = "Pending"
            };

            _context.DeliveryOrders.Add(deliveryOrder);
            await _context.SaveChangesAsync();
        }
    }
}