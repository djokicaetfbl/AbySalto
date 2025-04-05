using Newtonsoft.Json;

namespace AbySalto.Mid.Application.DTOs.CartDto
{
    public class AddToCartRequestDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("products")]
        public List<AddToCartProductDto> Products { get; set; } = new();
    }
}
