/// <summary>
/// Контракт репозитория для работы с заказами.
/// </summary>
namespace OrdersService.Repositories
{
    using OrdersService.Models;

    /// <summary>
    /// Определяет операции доступа к данным заказов.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Возвращает все заказы.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        Task<IEnumerable<OrderEntity>> GetAllAsync();

        /// <summary>
        /// Возвращает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Сущность заказа или null.</returns>
        Task<OrderEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Добавляет заказ.
        /// </summary>
        /// <param name="order">Сущность заказа.</param>
        Task AddAsync(OrderEntity order);

        /// <summary>
        /// Обновляет заказ.
        /// </summary>
        /// <param name="order">Сущность заказа.</param>
        void Update(OrderEntity order);

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        /// <param name="order">Сущность заказа.</param>
        void Remove(OrderEntity order);

        /// <summary>
        /// Возвращает суммарную стоимость всех заказов.
        /// </summary>
        /// <returns>Сумма стоимости.</returns>
        Task<decimal> GetTotalSumAsync();
    }
}