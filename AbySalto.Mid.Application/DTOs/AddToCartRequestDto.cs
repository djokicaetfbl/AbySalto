using Newtonsoft.Json;

namespace AbySalto.Mid.Application.DTOs
{
    public class AddToCartRequestDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("products")]
        public List<AddToCartProductDto> Products { get; set; } = new();
    }
}
