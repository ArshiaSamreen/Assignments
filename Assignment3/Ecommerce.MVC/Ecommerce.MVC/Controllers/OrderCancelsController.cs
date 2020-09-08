using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.MVC.Data;
using Ecommerce.MVC.Model;

namespace Ecommerce.MVC.Controllers
{
    public class OrderCancelsController : Controller
    {
        private readonly EcommerceDbContext _context;

        public OrderCancelsController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: OrderCancels
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderCancel.ToListAsync());
        }

        // GET: OrderCancels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCancel = await _context.OrderCancel
                .FirstOrDefaultAsync(m => m.OrderCancelID == id);
            if (orderCancel == null)
            {
                return NotFound();
            }

            return View(orderCancel);
        }

        // GET: OrderCancels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderCancels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderCancelID,CartProductID,QuantityToBeCancelled,RefundAmount")] OrderCancel orderCancel)
        {
            if (ModelState.IsValid)
            {
                orderCancel.OrderCancelID = Guid.NewGuid();
                _context.Add(orderCancel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderCancel);
        }

        // GET: OrderCancels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCancel = await _context.OrderCancel.FindAsync(id);
            if (orderCancel == null)
            {
                return NotFound();
            }
            return View(orderCancel);
        }

        // POST: OrderCancels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderCancelID,CartProductID,QuantityToBeCancelled,RefundAmount")] OrderCancel orderCancel)
        {
            if (id != orderCancel.OrderCancelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderCancel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderCancelExists(orderCancel.OrderCancelID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderCancel);
        }

        // GET: OrderCancels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCancel = await _context.OrderCancel
                .FirstOrDefaultAsync(m => m.OrderCancelID == id);
            if (orderCancel == null)
            {
                return NotFound();
            }

            return View(orderCancel);
        }

        // POST: OrderCancels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderCancel = await _context.OrderCancel.FindAsync(id);
            _context.OrderCancel.Remove(orderCancel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderCancelExists(Guid id)
        {
            return _context.OrderCancel.Any(e => e.OrderCancelID == id);
        }
    }
}
