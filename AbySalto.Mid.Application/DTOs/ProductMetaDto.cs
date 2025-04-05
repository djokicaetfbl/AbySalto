
namespace AbySalto.Mid.Application.DTOs
{
    public class ProductMetaDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Barcode { get; set; } = default!;
        public string QrCode { get; set; } = default!;
    }
}
