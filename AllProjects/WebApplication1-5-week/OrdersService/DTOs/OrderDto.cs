/// <summary>
/// DTO для передачи данных заказа через API и между слоями приложения.
/// </summary>
namespace OrdersService.DTOs
{
    /// <summary>
    /// Модель данных заказа для внешних запросов и ответов.
    /// </summary>
    public sealed class OrderDto
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
        /// Статус заказа в текстовом виде.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}