namespace AbySalto.Mid.Application.DTOs.ProductDto
{
    public class ProductResponseDto
    {
        public List<ProductDto> Products { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
