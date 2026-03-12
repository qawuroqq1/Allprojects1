﻿namespace DeliveryService.Repositories
{
    using DeliveryService.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Единица работы для сохранения изменений и доступа к репозиториям доставок.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext context;
        private readonly ILogger<UnitOfWork> logger;
        private bool disposed;

        /// <summary>
        /// Инициализирует новый экземпляр Unit of Work.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="deliveryOrders">Репозиторий доставок.</param>
        /// <param name="logger">Логгер.</param>
        public UnitOfWork(
            DeliveryDbContext context,
            IDeliveryRepository deliveryOrders,
            ILogger<UnitOfWork> logger)
        {
            this.context = context;
            this.DeliveryOrders = deliveryOrders;
            this.logger = logger;
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
            this.logger.LogInformation("Saving changes to DeliveryDb...");

            var result = await this.context.SaveChangesAsync();

            this.logger.LogInformation("SaveChanges completed. Rows affected: {RowsAffected}", result);

            return result;
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