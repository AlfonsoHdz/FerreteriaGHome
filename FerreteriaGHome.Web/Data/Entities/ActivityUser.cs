using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class ActivityUser
    {
        [Key]
        [Column(Order = 1)]
        public int ActivityId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
        public Activity Activity { get; set; }
        public User User { get; set; }
    }
}
