using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingDetail:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion del detalle de la Compra")]
        public string descripcionDC { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Costo de la Compra")]
        public double costC { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Fecha de la Compra")]
        public string DateShoppingDC { get; set; }

        public ICollection<Products> Products { get; set; }

        public ICollection<Shopping> Shoppings { get; set; }




    }
}
