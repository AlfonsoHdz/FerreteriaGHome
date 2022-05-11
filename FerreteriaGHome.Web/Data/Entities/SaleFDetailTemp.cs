using System.ComponentModel.DataAnnotations;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class SaleFDetailTemp : IEntity
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal UnitPrice { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal Amount { get { return this.UnitPrice * (decimal)this.Quantity; } }
    }
}
