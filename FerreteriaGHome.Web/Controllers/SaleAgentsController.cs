using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Models;
using FerreteriaGHome.Web.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FerreteriaGHome.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SaleAgentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper userHelper;

        public SaleAgentsController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesAgents
                .Include(t => t.User)
                .ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleAgentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByIdAsync(model.User.Id);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.User.FirstName,
                        LastName = model.User.LastName,
                        PhoneNumber = model.User.PhoneNumber,
                        Email = model.User.Email,
                        UserName = model.User.Email
                    };
                    var result = await userHelper.AddUserAsync(user, "123456");
                    await userHelper.AddUserToRoleAsync(user, "SalesAgent");
                    if (result == IdentityResult.Success)
                    {
                        var saleagent = new SaleAgent
                        {
                            Id = model.Id,

                            User = await this._context.Users.FindAsync(user.Id)
                        };
                        _context.Add(saleagent);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Fallido");
                }
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

            var saleagent = await this._context.SalesAgents
                .Include(u => u.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (saleagent == null)
            {
                return NotFound();
            }

            var model = new SaleAgentViewModel
            {
                Id = saleagent.Id,
                User = saleagent.User
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleAgentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._context.Users.FindAsync(model.User.Id);
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.Email = model.User.Email;
                user.PhoneNumber = model.User.PhoneNumber;

                user.UserName = model.User.Email;

                this._context.Update(user);
                await _context.SaveChangesAsync();

                var saleagent = new SaleAgent
                {
                    Id = model.Id,

                    User = await this._context.Users.FindAsync(model.User.Id)
                };

                this._context.Update(saleagent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleAgent = await _context.SalesAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleAgent == null)
            {
                return NotFound();
            }

            return View(saleAgent);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleAgent = await _context.SalesAgents.FindAsync(id);
            _context.SalesAgents.Remove(saleAgent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleAgentExists(int id)
        {
            return _context.SalesAgents.Any(e => e.Id == id);
        }
    }
}
