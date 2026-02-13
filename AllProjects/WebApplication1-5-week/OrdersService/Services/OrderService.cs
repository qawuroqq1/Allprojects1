using OrdersService.DTOs;
using OrdersService.Models;

namespace OrdersService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OrdersService.Models;
    using OrdersService.DTOs;
    using AutoMapper;

    public class OrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllAsync(OrderStatus? status)
        {
            var query = _context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            var entities = await query.ToListAsync();
            return _mapper.Map<List<OrderDto>>(entities);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<OrderDto>(entity);
        }

        public async Task<Guid> CreateAsync(OrderDto orderDto)
        {
            var entity = _mapper.Map<OrderEntity>(orderDto);
            entity.Id = Guid.NewGuid();
            entity.Status = OrderStatus.New;

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, OrderDto updatedDto)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (entity == null)
            {
                return false;
            }

            _mapper.Map(updatedDto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSumAsync(OrderStatus? status)
        {
            var query = _context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return await query.SumAsync(o => o.Price);
        }
    }
}