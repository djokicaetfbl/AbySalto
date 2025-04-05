
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Mid.Domain.Entities
{
    public class ShoppingCart
    {
        [Key]   
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal Total { get; set; }
        public decimal DiscountedTotal { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalProducts { get; set; }
    }
}
