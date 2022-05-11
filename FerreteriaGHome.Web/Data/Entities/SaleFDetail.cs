using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class SaleFDetail : IEntity
    {
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio unitario")]
        public decimal UnitPrice { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Importe")]

        public decimal Amount { get { return this.UnitPrice * (decimal)this.Quantity; } }

    }
}
