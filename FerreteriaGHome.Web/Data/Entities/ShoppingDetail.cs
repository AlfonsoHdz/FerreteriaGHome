using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingDetail : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion del detalle de la Compra")]
        public string Descripcion { get; set; }

        [Required]

        [Display(Name = "Costo de la Compra")]
        public decimal Cost { get; set; }

        [Required]

        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Fecha de la Compra")]
        public DateTime Date { get; set; }

        public Product Product { get; set; }

        public Shopping Shoppings { get; set; }




    }
}