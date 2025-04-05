using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.DTOs.CartDto
{
    public class ShoppingCartDto
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<ShoppingCartItem> Products { get; set; } = new List<ShoppingCartItem>();
        public decimal Total { get; set; }
        public decimal DiscountedTotal { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalProducts { get; set; }
    }
}
