namespace OrdersService.Repositories
{
    /// <summary>
    /// Реализация Unit of Work для управления транзакциями.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext context;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// Инициализирует новый экземпляр UnitOfWork.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="orders">Репозиторий заказов.</param>
        public UnitOfWork(AppDbContext context, IOrderRepository orders)
        {
            this.context = context;
            Orders = orders;
        }

        /// <summary>
        /// Репозиторий заказов.
        /// </summary>
        public IOrderRepository Orders { get; }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение управляемых и неуправляемых ресурсов.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }
    }
}