using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class ProyectStudentsController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProyectStudentsController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, RoleManager<IdentityRole> roleManager)
        {
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
            dataContext = context;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int poryectId)
        {
            //var studentsInPoryect = await dataContext.ProyectStudents
            //    .Where(ps => ps.ProyectId == poryectId)
            //    .Select(ps => ps.Student)
            //    .ToListAsync();


            return View(await dataContext.Users.Include(u => u.Role).ToListAsync());
        }
    }
}
