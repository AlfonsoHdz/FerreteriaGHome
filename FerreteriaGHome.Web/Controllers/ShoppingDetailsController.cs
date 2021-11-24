
namespace FerreteriaGHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using FerreteriaGHome.Web.Data;
    using FerreteriaGHome.Web.Data.Entities;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Admin")]
    public class ShoppingDetailsController : Controller
    {

        private readonly DataContext dataContext;

        public ShoppingDetailsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public async Task<IActionResult> Index()
        {
            return View(await this.dataContext.ShoppingDetails
                .ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await this.dataContext.ShoppingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }

            return View(shoppingDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoppingDetail shoppingDetail)
        {
            if (ModelState.IsValid)
            {
                this.dataContext.Add(shoppingDetail);
                await this.dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingDetail);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await this.dataContext.ShoppingDetails.FindAsync(id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }
            return View(shoppingDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ShoppingDetail shoppingDetail)
        {
            if (id != shoppingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.dataContext.Update(shoppingDetail);
                    await this.dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingDetailExists(shoppingDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingDetail);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingDetail = await dataContext.ShoppingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingDetail == null)
            {
                return NotFound();
            }

            return View(shoppingDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingDetail = await dataContext.ShoppingDetails.FindAsync(id);
            dataContext.ShoppingDetails.Remove(shoppingDetail);
            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingDetailExists(int id)
        {
            return this.dataContext.ShoppingDetails.Any(e => e.Id == id);
        }
    }
}
