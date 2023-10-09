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

       
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        [StringLength(6, ErrorMessage = "La {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
        public string CurrentPassword { get; set; }

       
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        [StringLength(6, ErrorMessage = "La {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem> roles { get; set; }
    }
}
