using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.Data;
using Ecommerce.API.Model;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderCancelsController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public OrderCancelsController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderCancels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderCancel>>> GetOrderCancel()
        {
            return await _context.OrderCancel.ToListAsync();
        }

        // GET: api/OrderCancels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderCancel>> GetOrderCancel(Guid id)
        {
            var orderCancel = await _context.OrderCancel.FindAsync(id);

            if (orderCancel == null)
            {
                return NotFound();
            }

            return orderCancel;
        }

        // PUT: api/OrderCancels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderCancel(Guid id, OrderCancel orderCancel)
        {
            if (id != orderCancel.OrderCancelID)
            {
                return BadRequest();
            }

            _context.Entry(orderCancel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderCancelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderCancels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderCancel>> PostOrderCancel(OrderCancel orderCancel)
        {
            _context.OrderCancel.Add(orderCancel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderCancel", new { id = orderCancel.OrderCancelID }, orderCancel);
        }

        // DELETE: api/OrderCancels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderCancel>> DeleteOrderCancel(Guid id)
        {
            var orderCancel = await _context.OrderCancel.FindAsync(id);
            if (orderCancel == null)
            {
                return NotFound();
            }

            _context.OrderCancel.Remove(orderCancel);
            await _context.SaveChangesAsync();

            return orderCancel;
        }

        private bool OrderCancelExists(Guid id)
        {
            return _context.OrderCancel.Any(e => e.OrderCancelID == id);
        }
    }
}
