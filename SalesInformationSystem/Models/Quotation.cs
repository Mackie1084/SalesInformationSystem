using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class Quotation
    {
        [Key]
        public int QuotationId { get; set; }
        public string CreatedBy { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public ICollection<SalesOrder> SalesOrder { get; set; }
        public ICollection<QuotationItem> QuotationItem { get; set; }
    }
}
