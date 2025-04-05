
namespace AbySalto.Mid.Application.DTOs
{
    public class ProductReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; } = default!;
        public string ReviewerEmail { get; set; } = default!;

    }
}
