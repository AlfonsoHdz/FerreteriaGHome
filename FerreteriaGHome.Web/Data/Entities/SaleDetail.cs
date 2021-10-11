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

        //TODO: Eliminar V y Mayuscula
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Fecha de venta")]

        //TODO: cambiar a tipo fecha
        public DateTime Date { get; set; }

        [Required]

        [Display(Name = "Precio de la Venta")]

        //TODO:Eliminar V y Mayuscula
        public decimal Price { get; set; }

        //TODO: Una sola venta
        public Sale Sales { get; set; }

        //TODO: Eliminar s
        //TODO: Cambiar el nombre clase Products a singular
        public Product Product { get; set; }


    }
}