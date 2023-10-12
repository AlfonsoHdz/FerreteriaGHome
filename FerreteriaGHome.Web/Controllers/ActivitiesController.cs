using System;
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
using System.IO;
using Microsoft.AspNetCore.Http;

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

        public IActionResult DownloadFile(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);

            if (activity != null && activity.File != null && activity.File.Length > 0)
            {
                return File(activity.File, "application/pdf", $"Evidencia {activity.Name}.pdf");
            }
            else
            {
                return NotFound();
            }
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
               

                var activity = new Activity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = await _context.Priorities.FindAsync(model.PriorityId),
                    Observations = model.Observations,
                    File = fileBytes
                   

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
            var model = new UpdateActivityViewModel
            {
                Name = activity.Name,
                Description = activity.Description,
                
                
                Observations = activity.Observations,
                PriorityId = activity.Priority.Id,
                Priority = activity.Priority,
                Priorities = this.combosHelper.GetComboPriorities()
            };

            //model.FileId = new FormFile(new MemoryStream(activity.File),0,activity.File.Length,"File",activity.Name);

           

            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateActivityViewModel model)
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


                var activity = new Activity
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
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> NewEvidence(int? id)
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

            var model = new UpdateActivityViewModel
            {
                Id = activity.Id,
                File = activity.File,
                
            };

           
            //model.CurrentFile = new FormFile(new MemoryStream(activity.File), 0, activity.File.Length, "File", "evidencia.pdf");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewEvidence(UpdateActivityViewModel model)
        {
            if(ModelState.IsValid)
            {
                var activity = await _context.Activities
                //.Include(a => a.Priority)
                .FirstOrDefaultAsync(a => a.Id == model.Id);

                if(activity == null)
                {
                    return NotFound();
                }

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
                    fileBytes = activity.File;
                }


                activity.File = fileBytes;
               

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
