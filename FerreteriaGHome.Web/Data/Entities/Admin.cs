using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Admin : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        public User User { get; set; }

        public ICollection<Shopping> Shoppings { get; set; }
    }
}