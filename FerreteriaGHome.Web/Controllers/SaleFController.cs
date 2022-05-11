using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Data.Entities;
using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    [Authorize(Roles = "Admin, Client")]
    public class SaleFController : Controller
    {
        private readonly DataContext datacontext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public SaleFController(
            DataContext datacontext,
            IUserHelper userHelper,
            ICombosHelper combosHelper)
        {
            this.datacontext = datacontext;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View(this.datacontext.SaleFs
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.User == user)
                .OrderBy(o => o.OrderDate));
        }
        public async Task<IActionResult> Create()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            var model = this.datacontext.SaleFDetailTemps
                .Include(od => od.Product)
                .Where(o => o.User == user);
            return View(model);
        }

        public IActionResult addProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = this.combosHelper.GetComboProducts()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> addProduct(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {

                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                var product = await this.datacontext.Products.FindAsync(model.ProductId);

                if (product == null)
                {
                    return NotFound();
                }

                var saleFDetailTemp = await this.datacontext.SaleFDetailTemps.Where(odt =>
                odt.User == user && odt.Product == product).FirstOrDefaultAsync();

                if (saleFDetailTemp == null)
                {
                    saleFDetailTemp = new Data.Entities.SaleFDetailTemp
                    {
                        Product = product,
                        Quantity = model.Quantity,
                        UnitPrice = product.Price,
                        User = user
                    };
                    this.datacontext.SaleFDetailTemps.Add(saleFDetailTemp);

                }
                else
                {
                    saleFDetailTemp.Quantity += model.Quantity;
                    this.datacontext.SaleFDetailTemps.Update(saleFDetailTemp);
                }

                await this.datacontext.SaveChangesAsync();

                return this.RedirectToAction("Create");

            }
            return this.View(model);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleFDetailTemp = await this.datacontext.SaleFDetailTemps.FindAsync(id);

            if (saleFDetailTemp == null)
            {
                return NotFound();
            }

            this.datacontext.SaleFDetailTemps.Remove(saleFDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleFDetailTemp = await this.datacontext.SaleFDetailTemps.FindAsync(id);

            if (saleFDetailTemp == null)
            {
                return NotFound();
            }
            saleFDetailTemp.Quantity += 1;
            this.datacontext.SaleFDetailTemps.Update(saleFDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleFDetailTemp = await this.datacontext.SaleFDetailTemps.FindAsync(id);

            if (saleFDetailTemp == null)
            {
                return NotFound();
            }
            saleFDetailTemp.Quantity -= 1;
            if (saleFDetailTemp.Quantity > 0)
            {
                this.datacontext.SaleFDetailTemps.Update(saleFDetailTemp);
                await this.datacontext.SaveChangesAsync();
            }
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var saleFDetailTemp = await this.datacontext.SaleFDetailTemps
                .Include(odt => odt.Product)
                .Where(odt => odt.User == user)
                .ToListAsync();

            if (saleFDetailTemp == null || saleFDetailTemp.Count == 0)
            {
                return NotFound();
            }

            var details = saleFDetailTemp.Select(odt => new SaleFDetail
            {
                UnitPrice = odt.UnitPrice,
                Product = odt.Product,
                Quantity = odt.Quantity


            }).ToList();

            var saleF = new SaleF
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                DeliveryDate = DateTime.UtcNow,
                Items = details
            };

            this.datacontext.SaleFs.Add(saleF);
            this.datacontext.SaleFDetailTemps.RemoveRange(saleFDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Index");

        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var salef = await datacontext.SaleFs.Where(u => u.User == user)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (salef == null)
            {
                return NotFound();
            }

            return View(salef);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salef = await datacontext.SaleFs.FindAsync(id);
            datacontext.SaleFs.Remove(salef);
            await datacontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrderExists(int id)
        {
            return datacontext.SaleFs.Any(e => e.Id == id);
        }


    }

}
