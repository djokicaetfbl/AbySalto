using AbySalto.Mid.Application.DTOs.ProductDto;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;
        private readonly DatabaseContext _context;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger, DatabaseContext context)
        {
            _httpClient = httpClient;
            _logger = logger;
            _context = context;
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

        public async Task<ProductResponseDto?> GetProductsAsyncDB(int skip = 0, int limit = 30, string sortBy = "title", bool descending = false)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                query = sortBy.ToLower() switch
                {
                    "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "rating" => descending ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating),
                    _ => descending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title)
                };

                var products = await query
                    .Skip(skip)
                    .Take(limit)
                    .AsNoTracking() 
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found in the database.");
                    return null;
                }

                var totalProducts = await query.CountAsync();

                var productResponseDto = new ProductResponseDto
                {
                    Products = products.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Category = p.Category,
                        Price = p.Price,
                        DiscountPercentage = p.DiscountPercentage,
                        Rating = p.Rating,
                        Stock = p.Stock,
                        Brand = p.Brand,
                        Sku = p.Sku,
                        Weight = p.Weight,
                        WarrantyInformation = p.WarrantyInformation,
                        ShippingInformation = p.ShippingInformation,
                        AvailabilityStatus = p.AvailabilityStatus,
                        ReturnPolicy = p.ReturnPolicy,
                        MinimumOrderQuantity = p.MinimumOrderQuantity,
                        Thumbnail = p.Thumbnail,
                        Images = p.Images
                    }).ToList(),
                    Total = totalProducts,
                    Skip = skip,
                    Limit = limit
                };

                return productResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products from the database.");
                throw;
            }
        }

    }
}
