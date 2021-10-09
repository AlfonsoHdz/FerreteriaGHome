using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Sales : IEntity
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Fecha de la Venta")]
        public string dateSale { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Descripcion de la Venta")]
        public string descriptionS { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Costo de la Venta")]
        public string costV { get; set; }

    }
}
