using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Models
{
    public class ProductViewModel:Product
    {
        [Display(Name = "Imagen")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Marca")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
