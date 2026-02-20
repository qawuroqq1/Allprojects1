namespace DeliveryService.Repositories;

public interface IUnitOfWork : IDisposable
{
    IDeliveryRepository DeliveryOrders { get; }

    Task<int> CompleteAsync();
}