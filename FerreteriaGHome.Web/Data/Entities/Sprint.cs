using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Sprint: IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDate { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
