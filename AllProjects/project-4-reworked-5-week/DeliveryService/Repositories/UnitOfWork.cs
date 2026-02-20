using DeliveryService.Models;

namespace DeliveryService.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DeliveryDbContext context;
    private bool disposed;

    public UnitOfWork(DeliveryDbContext context, IDeliveryRepository deliveryOrders)
    {
        this.context = context;
        this.DeliveryOrders = deliveryOrders;
    }

    public IDeliveryRepository DeliveryOrders { get; }

    public async Task<int> CompleteAsync()
    {
        return await this.context.SaveChangesAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            this.context.Dispose();
        }

        this.disposed = true;
    }
}