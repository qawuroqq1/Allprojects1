// <copyright file="IOrderService.cs" company="AllProjects">
// Copyright (c) AllProjects. All rights reserved.
// </copyright>

namespace OrdersService.Services
{
    using OrdersService.DTOs;

    /// <summary>
    /// Определяет операции бизнес-логики для заказов.
    /// </summary>
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(Guid id);

        Task<OrderDto> CreateAsync(OrderDto dto);

        Task<bool> UpdateAsync(Guid id, OrderDto dto);

        Task<bool> DeleteAsync(Guid id);

        Task<decimal> GetTotalSumAsync();
    }
}