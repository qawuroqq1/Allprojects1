/// <summary>
/// Контракт Unit of Work для DeliveryService.
/// </summary>
namespace DeliveryService.Repositories
{
    /// <summary>
    /// Определяет единицу работы для операций с доставками.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий доставок.
        /// </summary>
        IDeliveryRepository DeliveryOrders { get; }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <returns>Количество затронутых записей.</returns>
        Task<int> CompleteAsync();
    }
}