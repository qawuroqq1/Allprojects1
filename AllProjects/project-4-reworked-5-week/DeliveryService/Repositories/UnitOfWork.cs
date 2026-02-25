/// <summary>
/// Реализация Unit of Work для DeliveryService.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;

    /// <summary>
    /// Единица работы для сохранения изменений и доступа к репозиториям доставок.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext context;
        private bool disposed;

        /// <summary>
        /// Инициализирует новый экземпляр Unit of Work.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="deliveryOrders">Репозиторий доставок.</param>
        public UnitOfWork(DeliveryDbContext context, IDeliveryRepository deliveryOrders)
        {
            this.context = context;
            this.DeliveryOrders = deliveryOrders;
        }

        /// <summary>
        /// Репозиторий доставок.
        /// </summary>
        public IDeliveryRepository DeliveryOrders { get; }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <returns>Количество затронутых записей.</returns>
        public async Task<int> CompleteAsync()
        {
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
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