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
    public class ReturnsController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public ReturnsController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/Returns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Return>>> GetReturn()
        {
            return await _context.Return.ToListAsync();
        }

        // GET: api/Returns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Return>> GetReturn(Guid id)
        {
            var @return = await _context.Return.FindAsync(id);

            if (@return == null)
            {
                return NotFound();
            }

            return @return;
        }

        // PUT: api/Returns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReturn(Guid id, Return @return)
        {
            if (id != @return.OnlineReturnID)
            {
                return BadRequest();
            }

            _context.Entry(@return).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnExists(id))
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

        // POST: api/Returns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Return>> PostReturn(Return @return)
        {
            _context.Return.Add(@return);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReturn", new { id = @return.OnlineReturnID }, @return);
        }

        // DELETE: api/Returns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Return>> DeleteReturn(Guid id)
        {
            var @return = await _context.Return.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }

            _context.Return.Remove(@return);
            await _context.SaveChangesAsync();

            return @return;
        }

        private bool ReturnExists(Guid id)
        {
            return _context.Return.Any(e => e.OnlineReturnID == id);
        }
    }
}
