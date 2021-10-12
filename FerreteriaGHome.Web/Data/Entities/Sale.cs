using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Sale : IEntity
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha de la Venta")]

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion de la Venta")]

        public string Description { get; set; }

        [Required]

        [Display(Name = "Costo de la Venta")]
        public decimal Cost { get; set; }

        public Client Client { get; set; }

        public SaleAgent SalesAgent { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

    }
}