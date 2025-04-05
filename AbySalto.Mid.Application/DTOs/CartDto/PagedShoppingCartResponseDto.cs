namespace AbySalto.Mid.Application.DTOs.CartDto
{
    public class PagedShoppingCartResponseDto
    {
        public List<ShoppingCartItemDto> Carts { get; set; } = new List<ShoppingCartItemDto>();
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
