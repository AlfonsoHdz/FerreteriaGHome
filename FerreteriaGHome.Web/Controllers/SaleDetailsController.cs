
namespace FerreteriaGHome.Web.Controllers
{
    using FerreteriaGHome.Web.Data;
    using FerreteriaGHome.Web.Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin, SalesAgent")]
    public class SaleDetailsController : Controller
    {
        private readonly DataContext dataContext;

        public SaleDetailsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public async Task<IActionResult> Index()
        {
            return View(await dataContext.SaleDetails
                .ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await dataContext.SaleDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                dataContext.Add(saleDetail);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleDetail);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var saleDetail = await dataContext.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            return View(saleDetail);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, SaleDetail saleDetail)
        {
            if (id != saleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataContext.Update(saleDetail);
                    await dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.Id))
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
            return View(saleDetail);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await dataContext.SaleDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetail = await dataContext.SaleDetails.FindAsync(id);
            dataContext.SaleDetails.Remove(saleDetail);
            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SaleDetailExists(int id)
        {
            return dataContext.SaleDetails.Any(e => e.Id == id);
        }
    }
}
