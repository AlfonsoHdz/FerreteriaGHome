﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Client : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Sale> Sales { get; set; }





    }
}