using FerreteriaGHome.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class OrdersController: Controller
    {
        private readonly DataContext dataContext;

        public OrdersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await this.dataContext.Orders()
        //        );
        //}

    }
}
