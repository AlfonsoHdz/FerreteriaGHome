using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class SprintActivities : Controller
    {
        private readonly DataContext _context;

        public SprintActivities(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? Id, int? proyectId)
        {
            if (proyectId == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == proyectId);
            var sprint = await _context.Sprints.FirstOrDefaultAsync(p => p.Id == Id);


            if (proyect == null)
            {
                return NotFound();
            }

            var proyectCurrentId = proyect.Id;
            var sprintCurrentId = sprint.Id;

            ViewBag.proyectId = proyectCurrentId;
            ViewBag.Id = sprintCurrentId;

            var activities = _context.SprintActivities
               .Where(au => au.SprintId == Id)
               .Select(au => au.Activity)
               .ToList();

            var activityIds = activities.Select(au => au.Id).ToList();

            var priorities = _context.Activities
                .Where(a => activityIds.Contains(a.Id))
                .Include(a => a.Priority)
                .ToList();

            foreach(var activity in activities)
            {
                var priority = priorities.FirstOrDefault(p => p.Id == activity.Id);
                if (priority != null)
                    activity.Priority = priority.Priority;
            }

            return View(activities);
        }


        [HttpGet]
        public async Task<IActionResult> AddActivity(int Id, int proyectId)
        {
            if (Id == 0 || proyectId == 0)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == proyectId);
            var sprint = await _context.Sprints.FirstOrDefaultAsync(p => p.Id == Id);

            if (proyect == null || sprint == null)
            {
                return NotFound();
            }

            ViewBag.proyectId = proyect.Id;
            ViewBag.activityId = sprint.Id;

            var activityNotInSprint = _context.Activities
                .Include(p => p.Priority)
                .Where(u => !_context.SprintActivities.Any(au => au.SprintId == Id && au.ActivityId == u.Id))
                .Where(u => !_context.SprintActivities.Any(au => au.ActivityId == u.Id))
                .Where(u => _context.ProyectActivities.Any(pu => pu.ProyectId == proyectId && pu.ActivityId == u.Id))
                .ToList();

            return View(activityNotInSprint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivity(int Id, int proyectId, List<int> selectedActivitiesIds)
        {
            if (Id == 0 || proyectId == 0)
            {
                return NotFound();
            }

            if (selectedActivitiesIds != null && selectedActivitiesIds.Any())
            {
                foreach (var activityId in selectedActivitiesIds)
                {
                    var activity = await _context.Activities.FindAsync(activityId);

                    if (activity != null)
                    {
                        var existAssociation = await _context.SprintActivities
                            .Where(pu => pu.SprintId == Id && pu.ActivityId == activityId)
                            .FirstOrDefaultAsync();

                        if (existAssociation == null)
                        {
                            _context.SprintActivities.Add(new SprintActivity
                            {
                                SprintId = Id,
                                ActivityId = activityId
                            });
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
            }
            return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
        }

        public async Task<IActionResult> RemoveActivityFromSprint(int Id, int proyectId, int activityId)
        {
            var existAssociation = await _context.SprintActivities
               .Where(pu => pu.SprintId == Id && pu.ActivityId == activityId)
               .FirstOrDefaultAsync();

            if (existAssociation != null)
            {
                _context.SprintActivities.Remove(existAssociation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
        }
    }
}
