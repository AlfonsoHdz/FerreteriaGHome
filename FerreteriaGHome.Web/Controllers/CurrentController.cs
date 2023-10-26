﻿using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class CurrentController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        public CurrentController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;

        }

        [HttpGet]
        [Route("CurrentController")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var poryectName = proyect.Name;
            var proyectId = proyect.Id;

            ViewBag.proyectName = poryectName;
            ViewBag.proyectId = proyectId;

            var activitiesInProyect = await _context.ProyectActivities
                .Where(ps => ps.ProyectId == id)
                .Select(ps => new
                {
                    Activity = ps.Activity,
                    Priority = ps.Activity.Priority
                })
                .ToListAsync();

            var activitiesViewModel = activitiesInProyect.Select(item => new ActivityViewModel
            {
                Id = item.Activity.Id,
                Name = item.Activity.Name,
                Description = item.Activity.Description,
                Priority = item.Priority,
                Observations = item.Activity.Observations,
                File = item.Activity.File
            });

            return View(activitiesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateActivity(int? id)
        {
            var model = new ActivityViewModel
            {
                Priorities = combosHelper.GetComboPriorities()
            };

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var proyectId = proyect.Id;
            ViewBag.proyectId = proyectId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityViewModel model, int proyectId)
        {
            if (ModelState.IsValid)
            {
                byte[] fileBytes;

                if (model.FileId != null && model.FileId.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.FileId.CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }
                else
                {
                    fileBytes = new byte[0];
                }

                var activity = new ActivityViewModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = await _context.Priorities.FindAsync(model.PriorityId),
                    Observations = model.Observations,
                    File = fileBytes

                };

                _context.Add(activity);
                await _context.SaveChangesAsync();

                var proyectActivity = new ProyectActivity
                {
                    ProyectId = proyectId,
                    ActivityId = activity.Id
                };

                _context.ProyectActivities.Add(proyectActivity);
                await _context.SaveChangesAsync();  
                
                return RedirectToAction("Index", new { id = proyectId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditActivity(int? id, int? proyectId)
        {
            if (id == null && proyectId == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == proyectId);

            if(proyect == null)
            {
                return NotFound();
            }

            var proyect2 = proyect.Id;
            ViewBag.proyectId = proyect2;


            var activity = await _context.Activities
                .Include(p => p.Priority)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (activity == null)
            {
                return NotFound();
            }
            var model = new UpdateActivityViewModel
            {
                Name = activity.Name,
                Description = activity.Description,
                Observations = activity.Observations,
                PriorityId = activity.Priority.Id,
                Priority = activity.Priority,
                Priorities = this.combosHelper.GetComboPriorities()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActivity(UpdateActivityViewModel model, int? proyectId)
        {
            if (ModelState.IsValid)
            {
                byte[] fileBytes;

                if (model.FileId != null && model.FileId.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.FileId.CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();

                    }

                }
                else
                {
                    fileBytes = new byte[0];
                }


                var activity = new Data.Entities.Activity
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Priority = await _context.Priorities.FindAsync(model.PriorityId),
                    Observations = model.Observations,
                    File = fileBytes

                };

                _context.Update(activity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = proyectId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProyectDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var poryectoName = proyect.Name;

            ViewBag.proyectName = poryectoName;

            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> ProyActIndex(int? id)
        //{
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            //if (proyect == null)
            //{
            //    return NotFound();
            //}

            //var activitiesInProyect = await _context.ProyectActivities
            //    .Where(ps => ps.ProyectId == id)
            //    .Select(ps => ps.Activity)
            //    .ToListAsync();

            //return View(activitiesInProyect);

            
        //}

        
    }
}
