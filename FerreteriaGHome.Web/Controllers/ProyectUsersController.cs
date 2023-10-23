using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class ProyectUsersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProyectUsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, RoleManager<IdentityRole> roleManager)
        {
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
            dataContext = context;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("ProyectUsersController")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await dataContext.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var poryectName = proyect.Name;
            var proyectId = proyect.Id;

            ViewBag.proyectName = poryectName;
            ViewBag.proyectId = proyectId;


            var studentsInPoryect = await dataContext.ProyectUsers
                .Where(ps => ps.ProyectId == id)
                .Select(ps => ps.User)
                .ToListAsync();     

            return View(studentsInPoryect);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await dataContext.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var proyectId = proyect.Id;
            ViewBag.proyectId = proyectId;

            //Mostrar check de usuarios que ya esten asociados
            //var proyectUsers = await dataContext.ProyectUsers
            //    .Where(pu => pu.ProyectId == id)
            //    .Select(pu => pu.UserId)
            //    .ToListAsync();

            //var users = await dataContext.Users.Include(u => u.Role).ToListAsync();

            //return View(new AddUserViewModel
            //{
            //    Users = users,
            //    AssociatedUserIds = proyectUsers
            //});


            //return View(await dataContext.Users.Include(u => u.Role).ToListAsync());
            return View(await dataContext.Users
                .Where(u => u.Role.Name == "Student")
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(int proyectId, List<string> selectedUsersIds)
        {
            if(selectedUsersIds != null && selectedUsersIds.Any())
            {
                foreach(var userId in selectedUsersIds)
                {
                    var user = await dataContext.Users.FindAsync(userId);

                    if (user != null)
                    {
                        var existAssociation = await dataContext.ProyectUsers
                            .Where(pu => pu.ProyectId == proyectId && pu.UserId == userId)
                            .FirstOrDefaultAsync();

                        if(existAssociation == null)
                        {
                            dataContext.ProyectUsers.Add(new ProyectUser
                            {
                                ProyectId = proyectId,
                                UserId = userId
                            });
                        }
                    }
                }

                await dataContext.SaveChangesAsync();
                return RedirectToAction("Index", new {id = proyectId});
            }
            //return View();
            return RedirectToAction("Index", new {id = proyectId});
        }

        public async Task<IActionResult> RemoveUserFromProyect(int proyectId, string userId)
        {
            var proyectUser = await dataContext.ProyectUsers
                .Where(pu => pu.ProyectId == proyectId && pu.UserId == userId)
                .FirstOrDefaultAsync();

            if (proyectUser != null)
            {
                dataContext.ProyectUsers.Remove(proyectUser);
                await dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = proyectId});
        }
    }
}
