﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User: IdentityUser
    {
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Telefono")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{LastName} {FirstName}";
    }
}
