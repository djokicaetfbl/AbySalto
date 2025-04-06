using AbySalto.Mid.Application.DTOs.ProductDto;

namespace AbySalto.Mid.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto?> GetProductsAsync(int skip = 0, int limit = 30, string sortBy = "title", bool descending = false);
        Task<ProductResponseDto?> GetProductsAsyncDB(int skip = 0, int limit = 30, string sortBy = "title", bool descending = false);
        Task<ProductDto?> GetProductByIdAsync(int productId);
    }
}
