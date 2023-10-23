using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class ProyectStudent
    {
        [Key]
        [Column(Order = 1)]
        public int ProyectId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int StudentId { get; set; }

        public Proyect Proyect { get; set; }
        public Student Student { get; set; }

    }
}
