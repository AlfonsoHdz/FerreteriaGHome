using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    
    public class Product : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del Producto")]

        
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion del Producto")]
        public string Descripcion { get; set; }

        [Required]

        [Display(Name = "Precio del Producto")]
        public decimal Price { get; set; }


        public ICollection<SaleDetail> SaleDetail { get; set; }
        public ICollection<ShoppingDetail> ShoppingDetails { get; set; }




    }
}