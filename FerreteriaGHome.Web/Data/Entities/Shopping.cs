﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Shopping
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Folio de la Compra")]
        public string Folio { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Obervaciones de la Compra")]
        public string Observation { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Dato de la Compra")]
        public string DatoShopping { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "IVA")]
        public double IVA { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Total")]
        public double Total { get; set; }



    }
}