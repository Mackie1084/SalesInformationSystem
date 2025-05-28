using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class QuotationItem
    {
        [Key]
        public int QuotationItemId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }

        public int QuotationId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("QuotationId")]
        public Quotation Quotation { get; set; }
    }
}
