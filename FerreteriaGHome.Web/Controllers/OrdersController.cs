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
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContext datacontext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;

        public OrdersController(
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
            return View(this.datacontext.Orders
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
            var model = this.datacontext.OrderDetailTemps
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

                var orderDetailTemp = await this.datacontext.OrderDetailTemps.Where(odt =>
                odt.User == user && odt.Product == product).FirstOrDefaultAsync();

                if (orderDetailTemp == null)
                {
                    orderDetailTemp = new Data.Entities.OrderDetailTemp
                    {
                        Product = product,
                        Quantity = model.Quantity,
                        UnitPrice = product.Price,
                        User = user
                    };
                    this.datacontext.OrderDetailTemps.Add(orderDetailTemp);

                }
                else
                {
                    orderDetailTemp.Quantity += model.Quantity;
                    this.datacontext.OrderDetailTemps.Update(orderDetailTemp);
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

            var orderDetailTemp = await this.datacontext.OrderDetailTemps.FindAsync(id);

            if (orderDetailTemp == null)
            {
                return NotFound();
            }

            this.datacontext.OrderDetailTemps.Remove(orderDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailTemp = await this.datacontext.OrderDetailTemps.FindAsync(id);

            if (orderDetailTemp == null)
            {
                return NotFound();
            }
            orderDetailTemp.Quantity += 1;
            this.datacontext.OrderDetailTemps.Update(orderDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailTemp = await this.datacontext.OrderDetailTemps.FindAsync(id);

            if (orderDetailTemp == null)
            {
                return NotFound();
            }
            orderDetailTemp.Quantity -= 1;
            if (orderDetailTemp.Quantity > 0)
            {
                this.datacontext.OrderDetailTemps.Update(orderDetailTemp);
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

            var orderDetailTemp = await this.datacontext.OrderDetailTemps
                .Include(odt => odt.Product)
                .Where(odt => odt.User == user)
                .ToListAsync();

            if(orderDetailTemp == null || orderDetailTemp.Count == 0)
            {
                return NotFound();
            }

            var details = orderDetailTemp.Select(odt => new OrderDetail
            {
                UnitPrice = odt.UnitPrice,
                Product = odt.Product,
                Quantity = odt.Quantity


            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                DeliveryDate = DateTime.UtcNow,
                Items = details
            };

            this.datacontext.Orders.Add(order);
            this.datacontext.OrderDetailTemps.RemoveRange(orderDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Index");
            



        }
    }

}
