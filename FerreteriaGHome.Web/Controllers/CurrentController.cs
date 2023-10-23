using FerreteriaGHome.Web.Data;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Controllers
{
    public class CurrentController : Controller
    {
        private readonly DataContext _context;

        public CurrentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CurrentController")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var poryectName = proyect.Name;
            var proyectId = proyect.Id;

            ViewBag.proyectName = poryectName;
            ViewBag.proyectId = proyectId;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProyectDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyect = await _context.Proyects.FirstOrDefaultAsync(p => p.Id == id);

            if (proyect == null)
            {
                return NotFound();
            }

            var poryectoName = proyect.Name;

            ViewBag.proyectName = poryectoName;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
