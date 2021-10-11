using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;

namespace FerreteriaGHome.Web.Controllers
{
    public class ShoppingDetailsController : Controller
    {
        private readonly DataContext _context;

        public ShoppingDetailsController(DataContext context)
        {
            _context = context;
        }

        // GET: ShoppingDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingDetails.ToListAsync());
        }

        // GET: ShoppingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await _context.ShoppingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }

            return View(shoppingDetail);
        }

        // GET: ShoppingDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Cost,Quantity,Date")] ShoppingDetail shoppingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingDetail);
        }

        // GET: ShoppingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await _context.ShoppingDetails.FindAsync(id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }
            return View(shoppingDetail);
        }

        // POST: ShoppingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Cost,Quantity,Date")] ShoppingDetail shoppingDetail)
        {
            if (id != shoppingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingDetailExists(shoppingDetail.Id))
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
            return View(shoppingDetail);
        }

        // GET: ShoppingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await _context.ShoppingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }

            return View(shoppingDetail);
        }

        // POST: ShoppingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingDetail = await _context.ShoppingDetails.FindAsync(id);
            _context.ShoppingDetails.Remove(shoppingDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingDetailExists(int id)
        {
            return _context.ShoppingDetails.Any(e => e.Id == id);
        }
    }
}
