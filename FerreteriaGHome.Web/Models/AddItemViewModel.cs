using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Producto")]
        [Range(1,int.MaxValue,ErrorMessage ="Porfavor elija un producto")]
        public int ProductId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "Debe ser positivo")]
        public double Quantity { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
