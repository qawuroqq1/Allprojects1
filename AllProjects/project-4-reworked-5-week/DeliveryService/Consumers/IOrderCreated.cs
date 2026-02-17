namespace DeliveryService.Consumers;

public interface IOrderCreated
{
    Guid OrderId { get; }
    string Address { get; }
}