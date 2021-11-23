﻿using System;
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

namespace FerreteriaGHome.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper userHelper;

        public ClientsController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            this.userHelper = userHelper;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients
                .Include(t => t.User)
                .ToListAsync());
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel model)
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
                    await userHelper.AddUserToRoleAsync(user, "Client");
                    if (result == IdentityResult.Success)
                    {
                        var client = new Client
                        {
                            Id = model.Id,
                           
                            User = await this._context.Users.FindAsync(user.Id)
                        };
                        _context.Add(client);
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

            var client = await this._context.Clients
                .Include(u => u.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            var model = new ClientViewModel
            {
                Id = client.Id,
                User = client.User
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
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

                var client = new Client
                {
                    Id = model.Id,
                   
                    User = await this._context.Users.FindAsync(model.User.Id)
                };

                this._context.Update(client);
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

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
