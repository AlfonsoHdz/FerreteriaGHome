using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class ProyectUser
    {
        [Key]
        [Column(Order = 1)]
        public int ProyectId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public Proyect Proyect { get; set; }
        public User User { get; set; }
    }
}
