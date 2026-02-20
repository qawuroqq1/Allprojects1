using DeliveryService.Models;

namespace DeliveryService.Repositories;

public sealed class DeliveryRepository : IDeliveryRepository
{
    private readonly DeliveryDbContext context;

    public DeliveryRepository(DeliveryDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(DeliveryOrder deliveryOrder)
    {
        await this.context.DeliveryOrders.AddAsync(deliveryOrder).ConfigureAwait(false);
    }
}