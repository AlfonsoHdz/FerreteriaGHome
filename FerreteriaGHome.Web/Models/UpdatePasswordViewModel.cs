using FerreteriaGHome.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Models
{
    public class UpdatePasswordViewModel:User
    {
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
    }
}
