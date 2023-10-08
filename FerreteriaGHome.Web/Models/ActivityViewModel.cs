using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FerreteriaGHome.Web.Models
{
    public class ActivityViewModel: Activity
    {
        [Display(Name = "Prioridad")]
        public int PriorityId { get; set; }

        [Display(Name = "Evidencia")]
        public IFormFile FileId { get; set; }

        public IEnumerable<SelectListItem> Priorities { get; set; }
    }
}
