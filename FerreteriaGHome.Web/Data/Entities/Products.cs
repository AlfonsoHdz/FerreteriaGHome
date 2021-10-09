using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Products:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del Producto")]
        public string nameP { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion del Producto")]
        public string descripcionP { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Precio del Producto")]
        public double priceP { get; set; }


        public ICollection<SaleDetail> SaleDetail { get; set; }

        public ShoppingDetail ShoppingDetail { get; set; }




    }
}
