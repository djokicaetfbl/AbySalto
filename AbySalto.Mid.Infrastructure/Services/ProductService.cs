using AbySalto.Mid.Application.DTOs.ProductDto;
using AbySalto.Mid.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            try
            {
                var url = $"https://dummyjson.com/products/{productId}";
                var response = await _httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ProductDto>(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching product by ID.");
                throw;
            }
        }

        public async Task<ProductResponseDto?> GetProductsAsync(int skip = 0, int limit = 30, string sortBy = "title", bool descending = false)
        {
            try
            {
                var url = $"https://dummyjson.com/products?skip={skip}&limit={limit}&sortBy={sortBy}&descending={descending}";
                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from the product API.");
                    return null;
                }

                var productResponseDto = JsonConvert.DeserializeObject<ProductResponseDto>(response);

                if(productResponseDto == null)
                {
                    _logger.LogWarning("Failed to deserialize the product response.");
                    return null;
                }

                return productResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                throw;
            }
        }
       
    }
}
