/// <summary>
/// Реализация Unit of Work для DeliveryService.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;

    /// <summary>
    /// Единица работы для сохранения изменений и доступа к репозиториям доставок.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext _context;
        private bool _disposed;

        /// <summary>
        /// Инициализирует новый экземпляр Unit of Work.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="deliveryOrders">Репозиторий доставок.</param>
        public UnitOfWork(DeliveryDbContext context, IDeliveryRepository deliveryOrders)
        {
            _context = context;
            DeliveryOrders = deliveryOrders;
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
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}