using DeliveryService.Models;

namespace DeliveryService.Repositories;

public interface IDeliveryRepository
{
    Task AddAsync(DeliveryOrder deliveryOrder);
}