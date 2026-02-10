using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_3.Models;

namespace project_3.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        public DeliveryController(DeliveryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var deliveries = await _context.DeliveryOrders.ToListAsync();
            return Ok(deliveries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var delivery = await _context.DeliveryOrders.FindAsync(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }
    }
}