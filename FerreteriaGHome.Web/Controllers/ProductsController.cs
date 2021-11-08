namespace FerreteriaGHome.Web.Controllers
{
    using FerreteriaGHome.Web.Data;
    using FerreteriaGHome.Web.Data.Entities;
    using FerreteriaGHome.Web.Helper;
    using FerreteriaGHome.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    //  [Authorize(Roles = "SalesAgent, Client")]

    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;

        public ProductsController(DataContext context, ICombosHelper combosHelper, IImageHelper imageHelper)
        {
            _context = context;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(p => p.Brand)
                .ToListAsync());
        }


        // [Authorize (Roles = "SalesAgent")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        //[Authorize(Roles = "SalesAgent")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                Brands = this.combosHelper.GetComboBrands()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                    Brand = await _context.Brands.FindAsync(model.BrandId),
                    Descripcion = model.Descripcion,
                    
                    ImagenUrl = (model.ImageFile!=null?await imageHelper.UploadImageAsync(
                        model.ImageFile,
                        model.Name,
                        "products"): string.Empty)
                    
                };
                //
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var product = await _context.Products
                .Include(p => p.Brand)
                
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Descripcion = product.Descripcion,
                ImagenUrl = product.ImagenUrl,
                Stock = product.Stock,
                Brand = product.Brand,
                BrandId = product.Brand.Id,

                Brands = this.combosHelper.GetComboBrands()
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                    Brand = await _context.Brands.FindAsync(model.BrandId),
                    Descripcion = model.Descripcion,

                    ImagenUrl = (model.ImageFile != null ? await imageHelper.UploadImageAsync(
                        model.ImageFile,
                        model.Name,
                        "products") : model.ImagenUrl)

                };
                //
                _context.Update(product);
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
