namespace OrdersService.DTOs
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель заказа для API.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование заказа.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена заказа.
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}