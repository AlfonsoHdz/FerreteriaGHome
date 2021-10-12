using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{

    using System.ComponentModel.DataAnnotations;

    public class SaleDetail : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del detalle de Venta")]

        public string Name { get; set; }

        [Required]
        [Display(Name = "Fecha de venta")]

        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Precio de la Venta")]
        public decimal Price { get; set; }

       
        public Sale Sales { get; set; }

        public Product Product { get; set; }


    }
}