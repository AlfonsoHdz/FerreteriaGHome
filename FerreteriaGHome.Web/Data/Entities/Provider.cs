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
        public string namePro { get; set; }

       

        [Required]
        [MaxLength(300)]
        [Display(Name = "Direccion")]
        public string addressPro { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Telefono")]
        public long telephone { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Correo")]
        public string emailPro { get; set; }

        public ICollection<Shopping> Shoppings { get; set; }




    }
}
