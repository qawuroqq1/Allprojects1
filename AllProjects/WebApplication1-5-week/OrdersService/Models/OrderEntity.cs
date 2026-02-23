/// <summary>
/// Сущность заказа для хранения в базе данных.
/// </summary>
namespace OrdersService.Models
{
    /// <summary>
    /// Представляет заказ в системе.
    /// </summary>
    public sealed class OrderEntity
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование заказа.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Общая стоимость заказа.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Текущий статус заказа.
        /// </summary>
        public OrderStatus Status { get; set; }
    }
}