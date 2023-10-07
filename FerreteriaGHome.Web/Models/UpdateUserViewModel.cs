using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using FerreteriaGHome.Web.Data.Entities;


namespace FerreteriaGHome.Web.Models
{
    public class UpdateUserViewModel:User
    {
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string idRole { get; set; }

        public IEnumerable<SelectListItem> roles { get; set; }
    }
}
