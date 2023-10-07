using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Teacher:IEntity
    {
        [Display(Name = "Número de Empleado")]
        public int Id { get; set; }
        public User User { get; set; }
        public Proyect Proyect { get; set; }
    }
}
