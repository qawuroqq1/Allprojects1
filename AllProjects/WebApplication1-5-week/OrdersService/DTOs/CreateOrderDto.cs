// <copyright file="CreateOrderDto.cs" company="AllProjects">
// Copyright (c) AllProjects. All rights reserved.
// </copyright>

namespace OrdersService.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using OrdersService.Validation;

    /// <summary>
    /// Модель для создания нового заказа.
    /// </summary>
    public class CreateOrderDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        [OrderStatusValidation]
        public string Status { get; set; } = string.Empty;
    }
}
