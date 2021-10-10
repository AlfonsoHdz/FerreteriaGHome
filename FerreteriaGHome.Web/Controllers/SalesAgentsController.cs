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
    public class SalesAgentsController : Controller
    {
        private readonly DataContext _context;

        public SalesAgentsController(DataContext context)
        {
            _context = context;
        }

        // GET: SalesAgents
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesAgents.ToListAsync());
        }

        // GET: SalesAgents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesAgent == null)
            {
                return NotFound();
            }

            return View(salesAgent);
        }

        // GET: SalesAgents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesAgents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SalesAgent salesAgent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesAgent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesAgent);
        }

        // GET: SalesAgents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgents.FindAsync(id);
            if (salesAgent == null)
            {
                return NotFound();
            }
            return View(salesAgent);
        }

        // POST: SalesAgents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] SalesAgent salesAgent)
        {
            if (id != salesAgent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesAgent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesAgentExists(salesAgent.Id))
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
            return View(salesAgent);
        }

        // GET: SalesAgents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesAgent == null)
            {
                return NotFound();
            }

            return View(salesAgent);
        }

        // POST: SalesAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesAgent = await _context.SalesAgents.FindAsync(id);
            _context.SalesAgents.Remove(salesAgent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesAgentExists(int id)
        {
            return _context.SalesAgents.Any(e => e.Id == id);
        }
    }
}
