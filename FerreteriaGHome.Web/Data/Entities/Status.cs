using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Status
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
