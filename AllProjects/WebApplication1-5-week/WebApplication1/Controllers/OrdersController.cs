using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using MassTransit;
using Contracts;

namespace WebApplication1.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new List<Order>();
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            order.Id = Guid.NewGuid();
            _orders.Add(order);

            await _publishEndpoint.Publish<IOrderCreated>(new
            {
                OrderId = order.Id,
                Address = "Some Address"
            });

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null) return NotFound();

            existingOrder.Name = order.Name;
            existingOrder.Price = order.Price;
            existingOrder.Status = order.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            _orders.Remove(order);
            return NoContent();
        }
    }
}

namespace Contracts
{
    public interface IOrderCreated
    {
        Guid OrderId { get; }
        string Address { get; }
    }
}