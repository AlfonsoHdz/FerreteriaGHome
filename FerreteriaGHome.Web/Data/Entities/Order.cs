using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de orden")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de entrega")]
        public DateTime DeliveryDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de orden")]
        public User User { get; set; }

        public IEnumerable<OrderDetail> Items { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public double Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity); } }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Total")]
        public decimal Total { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Amount); } }
    }
}
