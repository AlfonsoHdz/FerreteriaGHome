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
    public class SaleAgentsController : Controller
    {
        private readonly DataContext _context;

        public SaleAgentsController(DataContext context)
        {
            _context = context;
        }

        // GET: SaleAgents
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesAgents
                .Include(t => t.User)
                .ToListAsync());
        }

        // GET: SaleAgents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleAgent = await _context.SalesAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleAgent == null)
            {
                return NotFound();
            }

            return View(saleAgent);
        }

        // GET: SaleAgents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleAgents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SaleAgent saleAgent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleAgent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleAgent);
        }

        // GET: SaleAgents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleAgent = await _context.SalesAgents.FindAsync(id);
            if (saleAgent == null)
            {
                return NotFound();
            }
            return View(saleAgent);
        }

        // POST: SaleAgents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] SaleAgent saleAgent)
        {
            if (id != saleAgent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleAgent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleAgentExists(saleAgent.Id))
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
            return View(saleAgent);
        }

        // GET: SaleAgents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleAgent = await _context.SalesAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleAgent == null)
            {
                return NotFound();
            }

            return View(saleAgent);
        }

        // POST: SaleAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleAgent = await _context.SalesAgents.FindAsync(id);
            _context.SalesAgents.Remove(saleAgent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleAgentExists(int id)
        {
            return _context.SalesAgents.Any(e => e.Id == id);
        }
    }
}
