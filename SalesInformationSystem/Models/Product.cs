using System.ComponentModel.DataAnnotations;

namespace SalesInformationSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int StockQuantity { get; set; }
                
        public ICollection<QuotationItem> QuotationItem { get; set; }
        public ICollection<CartItems> CartItems { get; set; }

    }
}
