using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    //TODO: Cambiar el nombre clase Products a singular
    public class Product : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del Producto")]

        //TODO: Eliminar P y Mayuscula inicial
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion del Producto")]
        //TODO: Eliminar P y Mayuscula
        public string Descripcion { get; set; }

        [Required]

        [Display(Name = "Precio del Producto")]
        //TODO: Eliminar P y Mayuscula
        //Cambiar a tipo decimal
        public decimal Price { get; set; }


        public ICollection<SaleDetail> SaleDetail { get; set; }
        public ICollection<ShoppingDetail> ShoppingDetails { get; set; }




    }
}