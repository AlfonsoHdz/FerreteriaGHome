using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Nombre")]
        public string firstnameU { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Apellido")]
        public string lastNameU { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Direccion")]
        public string addressU { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Telefono")]
        public int telephoneU { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Correo")]
        public string emailU { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{lastNameU} {firstnameU}";
    }
}
