using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Activity : IEntity
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

        public Priority Priority { get; set; }

        public Sprint Sprint { get; set; }

        [Display(Name = "Evidencia")]
        public byte[] File { get; set; }

        
        [MaxLength(40, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        public ICollection<ProyectActivity> ProyectActivities { get; set; }

    }
}
