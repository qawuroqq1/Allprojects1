/// <summary>
/// Репозиторий заказов на основе Entity Framework Core.
/// </summary>
namespace OrdersService.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using OrdersService.Models;

    /// <summary>
    /// Реализация репозитория заказов.
/// </summary>
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Возвращает все заказы.
/// </summary>
/// <returns>Коллекция заказов.</returns>
        public async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await this.context.Orders.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Возвращает заказ по идентификатору.
/// </summary>
/// <param name="id">Идентификатор заказа.</param>
/// <returns>Сущность заказа или null.</returns>
        public async Task<OrderEntity?> GetByIdAsync(Guid id)
        {
            return await this.context.Orders.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Добавляет заказ.
/// </summary>
/// <param name="order">Сущность заказа.</param>
        public async Task AddAsync(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            await this.context.Orders.AddAsync(order).ConfigureAwait(false);
        }

        /// <summary>
        /// Обновляет заказ.
/// </summary>
/// <param name="order">Сущность заказа.</param>
        public void Update(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            this.context.Orders.Update(order);
        }

        /// <summary>
        /// Удаляет заказ.
/// </summary>
/// <param name="order">Сущность заказа.</param>
        public void Remove(OrderEntity order)
        {
            ArgumentNullException.ThrowIfNull(order);

            this.context.Orders.Remove(order);
        }

        /// <summary>
        /// Возвращает суммарную стоимость всех заказов.
/// </summary>
/// <returns>Сумма стоимости.</returns>
        public async Task<decimal> GetTotalSumAsync()
        {
            return await this.context.Orders.SumAsync(x => x.Price).ConfigureAwait(false);
        }
    }
}