namespace AbySalto.Mid.Application.DTOs
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string? Thumbnail { get; set; }
    }
}
