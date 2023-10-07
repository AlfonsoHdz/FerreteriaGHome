﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FerreteriaGHome.Web.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
      

        public ActivitiesController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;

        }

        // GET: Activities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Activities
               .Include(p => p.Priority)
               .ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            var model = new ActivityViewModel
            {
                Priorities = this.combosHelper.GetComboPriorities()
            };
            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel model)
        {

            if (ModelState.IsValid)
            {
                var activity = new Activity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = await _context.Priorities.FindAsync(model.PriorityId),
                   

                };

                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(p => p.Priority)

                .FirstOrDefaultAsync(p => p.Id == id);
            if (activity == null)
            {
                return NotFound();
            }
            var model = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Priorities = this.combosHelper.GetComboPriorities()
            };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ActivityViewModel model)
        {

            if (ModelState.IsValid)
            {
                var activity = new Activity
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                 
                    Priority = await _context.Priorities.FindAsync(model.PriorityId),
                  

                };

                _context.Update(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}