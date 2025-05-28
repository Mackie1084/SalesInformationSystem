using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class SalesOrder
    {
        [Key]
        public int SalesOrderId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string Status { get; set; }

        public int QuotationId { get; set; }
        public int InvoiceId { get; set; }

        [ForeignKey("QuotationId")]
        public Quotation Quotation { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
    }
}
