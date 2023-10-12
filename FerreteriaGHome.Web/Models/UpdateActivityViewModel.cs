using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Models    
{
    public class UpdateActivityViewModel:Activity
    {
        [Display(Name = "Evidencia")]
        public IFormFile FileId { get; set; }

        [Display(Name = "Prioridad")]
        public int PriorityId { get; set; }

        public IEnumerable<SelectListItem> Priorities { get; set; }

    }
}
