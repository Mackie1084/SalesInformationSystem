using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;

namespace SalesInformationSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustLname { get; set; }
        public string CustFname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<Quotation> Quotation { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
    }
}
