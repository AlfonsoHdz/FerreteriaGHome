using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class SprintActivity
    {
        [Key]
        [Column(Order = 1)]
        public int SprintId { get; set;}
        [Key]
        [Column(Order = 2)]
        public int ActivityId { get; set;}

        public Sprint Sprint { get; set;}
        public Activity Activity { get; set;}
    }
}
