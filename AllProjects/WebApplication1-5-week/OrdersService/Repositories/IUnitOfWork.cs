/// <summary>
/// Контракт Unit of Work для объединения репозиториев и сохранения изменений.
/// </summary>
namespace OrdersService.Repositories
{
    /// <summary>
    /// Определяет единицу работы для операций с заказами.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий заказов.
        /// </summary>
        IOrderRepository Orders { get; }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <returns>Количество затронутых записей.</returns>
        Task<int> CompleteAsync();
    }
}