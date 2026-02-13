using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersService.DTOs;
using OrdersService.Models;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private static List<OrderEntity> _orders = new List<OrderEntity>();

        public OrdersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dtos = _mapper.Map<List<OrderDto>>(_orders);
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create(OrderDto orderDto)
        {
            var entity = _mapper.Map<OrderEntity>(orderDto);
            entity.Id = Guid.NewGuid();
            _orders.Add(entity);
            return Ok(_mapper.Map<OrderDto>(entity));
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, OrderDto orderDto)
        {
            var existing = _orders.FirstOrDefault(o => o.Id == id);
            if (existing == null) return NotFound();

            _mapper.Map(orderDto, existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _orders.FirstOrDefault(o => o.Id == id);
            if (existing == null) return NotFound();

            _orders.Remove(existing);
            return Ok(new { Message = "Deleted" });
        }
    }
}