namespace AbySalto.Mid.Application.DTOs.ProductDto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string Brand { get; set; }
        public string Sku { get; set; } = default!;
        public string Weight { get; set; } = default!;
        public DimensionsDto Dimensions { get; set; } = new();
        public string WarrantyInformation { get; set; } = default!;
        public string ShippingInformation { get; set; } = default!;
        public string AvailabilityStatus { get; set; } = default!;
        public List<ProductReviewDto> Reviews { get; set; } = new();
        public string ReturnPolicy { get; set; } = default!;
        public string MinimumOrderQuantity { get; set; } = default!;
        public ProductMetaDto Meta { get; set; } = new();
        public string Thumbnail { get; set; }
        public List<string> Images { get; set; } = new();
    }
}
