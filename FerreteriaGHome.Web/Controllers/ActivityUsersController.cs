using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class ActivityUsersController : Controller
    {
        private readonly DataContext _context;

        public ActivityUsersController(DataContext context)
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
            var activity = await _context.Activities.FirstOrDefaultAsync(p => p.Id == Id);


            if (proyect == null)
            {
                return NotFound();
            }

            var proyectCurrentId = proyect.Id;
            var activityCurrentId = activity.Id;

            ViewBag.proyectId = proyectCurrentId;
            ViewBag.Id = activityCurrentId;

            var usersInActivity = _context.ActivityUsers
                .Where(au => au.ActivityId == Id)
                .Select(au => au.User)
                .ToList();

            return View(usersInActivity);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser(int Id, int proyectId)
        {
            if (Id == 0 || proyectId == 0)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == proyectId);
            var activity = await _context.Activities.FirstOrDefaultAsync(p => p.Id == Id);

            if (proyect == null || activity == null)
            {
                return NotFound();
            }

            ViewBag.proyectId = proyect.Id;
            ViewBag.activityId = activity.Id;

            var usersNotInActivity = _context.Users
                .Where(u => !_context.ActivityUsers.Any(au => au.ActivityId == Id && au.UserId == u.Id))
                .Where(u => u.Role.Name == "Student")   
                .ToList();

            return View(usersNotInActivity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(int Id, int proyectId, List<string> selectedUsersIds)
        {
            if(Id == 0 || proyectId == 0)
            {
                return NotFound();
            }

            if(selectedUsersIds != null && selectedUsersIds.Any())
            {
                foreach(var userId in selectedUsersIds)
                {
                    var user = await _context.Users.FindAsync(userId);

                    if (user != null)
                    {
                        var existAssociation = await _context.ActivityUsers
                            .Where(pu => pu.ActivityId == Id && pu.UserId == userId)
                            .FirstOrDefaultAsync();

                        if (existAssociation == null)
                        {
                            _context.ActivityUsers.Add(new ActivityUser
                            {
                                ActivityId = Id,
                                UserId = userId
                            });
                        }
                    } 
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
            }
            return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
        }


        public async Task<IActionResult> RemoveUserFromActivity(int Id, int proyectId, string userId)
        {
            var existAssociation = await _context.ActivityUsers
                .Where(pu => pu.ActivityId == Id && pu.UserId == userId)
                .FirstOrDefaultAsync();

            if(existAssociation != null)
            {
                _context.ActivityUsers.Remove(existAssociation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = Id, proyectid = proyectId });
        }
    }
}
