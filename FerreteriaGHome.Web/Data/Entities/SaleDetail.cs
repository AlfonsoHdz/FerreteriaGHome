﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{

    using System.ComponentModel.DataAnnotations;

    public class SaleDetail:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del detalle de Venta")]
        public string nameV { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Año")]
        public int yearV { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Precio de la Venta")]
        public double priceV { get; set; }



    }
}
