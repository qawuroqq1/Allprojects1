// <copyright file="OrderDto.cs" company="AllProjects">
// Copyright (c) AllProjects. All rights reserved.
// </copyright>

namespace OrdersService.DTOs
{
    /// <summary>
    /// Модель данных заказа для внешних запросов и ответов.
    /// </summary>
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}