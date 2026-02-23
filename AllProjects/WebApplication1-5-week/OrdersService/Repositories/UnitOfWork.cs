/// <summary>
/// Реализация Unit of Work для OrdersService.
/// </summary>
namespace OrdersService.Repositories
{
    /// <summary>
    /// Единица работы для сохранения изменений и доступа к репозиториям.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private bool disposed;

        /// <summary>
        /// Инициализирует новый экземпляр Unit of Work.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="orders">Репозиторий заказов.</param>
        public UnitOfWork(AppDbContext context, IOrderRepository orders)
        {
            this.context = context;
            this.Orders = orders;
        }

        /// <summary>
        /// Репозиторий заказов.
        /// </summary>
        public IOrderRepository Orders { get; }

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