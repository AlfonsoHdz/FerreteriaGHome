using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Provider : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Required]

        [Display(Name = "Telefono")]
        public long Telephone { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        public ICollection<Shopping> Shoppings { get; set; }




    }
}