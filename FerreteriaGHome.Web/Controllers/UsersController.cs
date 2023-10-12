using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using FerreteriaGHome.Web.Models;

namespace FerreteriaGHome.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, RoleManager<IdentityRole> roleManager)
        {
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
            dataContext = context;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await dataContext.Users.Include(u => u.Role).ToListAsync());
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUser = await dataContext.Users.Include(u => u.Role).FirstOrDefaultAsync(m => m.Id == id);
            if (cUser == null)
            {
                return NotFound();
            }

            return View(cUser);
        }


        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUser = await dataContext.Users.Include(u => u.Role).FirstOrDefaultAsync(m => m.Id == id);
            if (cUser == null)
            {
                return NotFound();
            }
            var model = new UpdateUserViewModel
            {
                FirstName = cUser.FirstName,
                FathersName = cUser.FathersName,
                MaternalName = cUser.MaternalName,
                Email = cUser.Email,
                UserName = cUser.Email,
                idRole = cUser.Role.Id,
                Role = cUser.Role,
                roles = this.combosHelper.GetComboRoles()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var cUser = await dataContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == model.Id);

                cUser.FirstName = model.FirstName;
                cUser.FathersName = model.FathersName;
                cUser.MaternalName = model.MaternalName;
                cUser.Email = model.Email;
                cUser.UserName = model.Email;
                cUser.Role = await this.dataContext.Roles.FindAsync(model.idRole);

                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var changePasswordRes = await userHelper.ChangePasswordAsync(cUser, model.CurrentPassword, model.NewPassword);

                    if(!changePasswordRes.Succeeded)
                    {
                        foreach (var error in changePasswordRes.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        model.roles = this.combosHelper.GetComboRoles();
                        return View(model);
                    }
                }

                var result = await userHelper.UpdateUserAsync(cUser);
    
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se ha podido añadir el usuario");
                }
                
                await userHelper.AddUserToRoleAsync(cUser, cUser.Role.Name);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public IActionResult Create()
        {
            var model = new AddUserViewModel
            {
                roles = this.combosHelper.GetComboRoles()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByIdAsync(model.Id);
                user ??= new User
                {
                    FirstName = model.FirstName,
                    FathersName = model.FathersName,
                    MaternalName = model.MaternalName,
                    Email = model.Email,
                    UserName = model.Email,
                    Role = await this.dataContext.Roles.FindAsync(model.idRole)
                };
                var result = await userHelper.AddUserAsync(user, model.Pass);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se ha podido añadir el usuario");
                }
                await userHelper.AddUserToRoleAsync(user, user.Role.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUser = await dataContext.Users
                .Include(b => b.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (cUser == null)
            {
                return NotFound();
            }

            return View(cUser);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cUser = await this.dataContext.Users.FindAsync(id);
            dataContext.Users.Remove(cUser);
            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
