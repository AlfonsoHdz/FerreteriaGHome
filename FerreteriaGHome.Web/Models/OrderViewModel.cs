using FerreteriaGHome.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Models
{
    public class OrderViewModel:Order
    {
        public int ItemId { get; set; }

        public int OrderId { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
       

    }
}
