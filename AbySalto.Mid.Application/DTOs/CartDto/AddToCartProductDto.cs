using Newtonsoft.Json;
using System.Text.Json;

namespace AbySalto.Mid.Application.DTOs.CartDto
{
    public class AddToCartProductDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
