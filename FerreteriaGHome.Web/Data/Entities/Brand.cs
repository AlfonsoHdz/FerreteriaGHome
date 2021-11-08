using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Brand: IEntity
    {
        public int Id { get; set; }

       
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Marca")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }


    }
}
