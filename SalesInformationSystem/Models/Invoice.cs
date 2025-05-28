using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public string PaymentStatus { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


        public ICollection<SalesOrder> SaleOrder { get; set; }
        public ICollection<Payment> Payment { get; set; }

    }
}
