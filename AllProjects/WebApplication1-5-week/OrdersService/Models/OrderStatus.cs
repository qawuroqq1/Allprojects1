/// <summary>
/// Перечисление возможных статусов заказа.
/// </summary>
namespace OrdersService.Models
{
    /// <summary>
    /// Статусы жизненного цикла заказа.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Заказ создан.
        /// </summary>
        Created = 0,

        /// <summary>
        /// Заказ оплачен.
        /// </summary>
        Paid = 1,

        /// <summary>
        /// Заказ отменён.
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// Заказ завершён.
        /// </summary>
        Completed = 3,
    }
}