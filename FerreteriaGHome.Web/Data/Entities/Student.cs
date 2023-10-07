using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Student:IEntity
    {
        [Display(Name = "Número de Empleado")]
        public int Id { get; set; }
        public User User { get; set; }
        public Career Carrer { get; set; }
        public Proyect Proyect { get; set; }
    }
}
