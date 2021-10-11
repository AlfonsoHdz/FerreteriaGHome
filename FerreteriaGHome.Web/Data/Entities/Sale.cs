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
        [MaxLength(300)]
        [Display(Name = "Fecha de la Venta")]

        //TODO: CAMBIAR A TIPO FECHA/HORA
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion de la Venta")]

        //TODO: Eliminar la S
        public string Description { get; set; }

        [Required]

        //TODO: Cambiar a precio
        [Display(Name = "Costo de la Venta")]
        public decimal Cost { get; set; }


        //TODO: UN SOLO CLIENTE
        public Client Client { get; set; }

        public SaleAgent SalesAgent { get; set; }

        //TODO: CAMBIAR A ICOLLECTION
        public ICollection<SaleDetail> SaleDetails { get; set; }

    }
}