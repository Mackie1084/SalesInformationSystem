using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class CartItems
    {
        [Key]
        public int CartPk { get; set; }
        public string CartId { get; set; }
        public int ItemId { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
