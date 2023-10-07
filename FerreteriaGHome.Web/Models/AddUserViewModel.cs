using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using FerreteriaGHome.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Models
{
    public class AddUserViewModel:User
    {
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Pass { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string idRole { get; set; }

        public IEnumerable<SelectListItem> roles { get; set; }

    }
}
