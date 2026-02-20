/// <summary>
/// </summary>
namespace OrdersService.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private bool disposed;

        public UnitOfWork(AppDbContext context, IOrderRepository orders)
        {
            this.context = context;
            this.Orders = orders;
        }

        public IOrderRepository Orders { get; }

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
}