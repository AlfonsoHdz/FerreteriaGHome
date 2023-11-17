using FerreteriaGHome.Web.Data.Entities;
using System.Collections;
using System.Collections.Generic;

namespace FerreteriaGHome.Web.Models
{
    public class ProyectViewModel
    {
        public IEnumerable<ActivityViewModel> Activities { get; set; }
        public IEnumerable<Sprint> Sprints { get; set; }


    }
}
