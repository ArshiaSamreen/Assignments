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
    public class CartProductsController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public CartProductsController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/CartProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProduct>>> GetCartProduct()
        {
            return await _context.CartProduct.ToListAsync();
        }

        // GET: api/CartProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartProduct>> GetCartProduct(Guid id)
        {
            var cartProduct = await _context.CartProduct.FindAsync(id);

            if (cartProduct == null)
            {
                return NotFound();
            }

            return cartProduct;
        }

        // PUT: api/CartProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartProduct(Guid id, CartProduct cartProduct)
        {
            if (id != cartProduct.CartProductID)
            {
                return BadRequest();
            }

            _context.Entry(cartProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartProductExists(id))
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

        // POST: api/CartProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CartProduct>> PostCartProduct(CartProduct cartProduct)
        {
            _context.CartProduct.Add(cartProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartProduct", new { id = cartProduct.CartProductID }, cartProduct);
        }

        // DELETE: api/CartProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartProduct>> DeleteCartProduct(Guid id)
        {
            var cartProduct = await _context.CartProduct.FindAsync(id);
            if (cartProduct == null)
            {
                return NotFound();
            }

            _context.CartProduct.Remove(cartProduct);
            await _context.SaveChangesAsync();

            return cartProduct;
        }

        private bool CartProductExists(Guid id)
        {
            return _context.CartProduct.Any(e => e.CartProductID == id);
        }
    }
}
