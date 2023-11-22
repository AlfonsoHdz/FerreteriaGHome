using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class CurrentSprintController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;

        public CurrentSprintController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
        }

        [Route("CurrentSprintController")]
        [HttpGet]
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

            DateTime currentDate = DateTime.Now;

            var currentSprint = await _context.ProyectSprints
                .Where(ps => ps.ProyectId == proyectId)
                .Select(ps => ps.Sprint)
                .FirstOrDefaultAsync(s => s.StartDate <= currentDate && s.EndDate > currentDate);

            ViewBag.currentSprint = currentSprint;


            if (currentSprint == null)
            {
                return NotFound("No existe algun Sprint en curso por el momento");
            }

            var activitiesInCurrentSprint =  _context.SprintActivities
                .Where(sa => sa.SprintId == currentSprint.Id)
                .Select(sa => sa.Activity)
                .ToList();

            var activityIds = activitiesInCurrentSprint.Select(au => au.Id).ToList();

            var priorities = _context.Activities
                .Where(a => activityIds.Contains(a.Id))
                .Include(a => a.Priority)
                .ToList();

            var statuses = _context.Activities
                .Where(a => activityIds.Contains(a.Id))
                .Include(a => a.Status)
                .ToList();

            foreach (var activity in activitiesInCurrentSprint)
            {
                var priority = priorities.FirstOrDefault(p => p.Id == activity.Id);
                var status = statuses.FirstOrDefault(s => s.Id == activity.Id);

                if (priority != null || status != null)
                {
                    activity.Priority = priority.Priority;
                    activity.Status = status.Status;
                }
                    
            }


            return View(activitiesInCurrentSprint);
        }


        [HttpGet]
        public async Task<IActionResult> EditStateFromActivity(int? id, int? proyectId)
        {
            if (id == null && proyectId == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == proyectId);

            if (proyect == null)
            {
                return NotFound();
            }

            var proyect2 = proyect.Id;
            ViewBag.proyectId = proyect2;


            var activity = await _context.Activities
                .Include(p => p.Priority)
                .Include(s => s.Status)
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
                Priorities = this.combosHelper.GetComboPriorities(),
                StateId = activity.Status.Id,
                Status = activity.Status,
                Statuses = this.combosHelper.GetComboStatuses()
                

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStateFromActivity(UpdateActivityViewModel model, int? proyectId)
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
                    File = fileBytes,
                    Status = await _context.Statuses.FindAsync(model.StateId)

                };

                _context.Update(activity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = proyectId });
            }

            return View(model);
        }
    }
}
