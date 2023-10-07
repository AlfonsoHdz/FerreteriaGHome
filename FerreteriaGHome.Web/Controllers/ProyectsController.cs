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
    public class ProyectsController : Controller
    {
        private readonly DataContext _context;

        public ProyectsController(DataContext context)
        {
            _context = context;
        }

        // GET: Proyects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proyects.ToListAsync());
        }

        // GET: Proyects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyect == null)
            {
                return NotFound();
            }

            return View(proyect);
        }

        // GET: Proyects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate")] Proyect proyect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyect);
        }

        // GET: Proyects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FindAsync(id);
            if (proyect == null)
            {
                return NotFound();
            }
            return View(proyect);
        }

        // POST: Proyects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate")] Proyect proyect)
        {
            if (id != proyect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectExists(proyect.Id))
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
            return View(proyect);
        }

        // GET: Proyects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyect == null)
            {
                return NotFound();
            }

            return View(proyect);
        }

        // POST: Proyects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyect = await _context.Proyects.FindAsync(id);
            _context.Proyects.Remove(proyect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectExists(int id)
        {
            return _context.Proyects.Any(e => e.Id == id);
        }
    }
}
