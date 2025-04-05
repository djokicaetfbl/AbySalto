using AbySalto.Mid.Application.DTOs.CartDto;

namespace AbySalto.Mid.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<bool> AddProductToShoppingCartAsyncDB(int applicationUserId, int productId, int quantity);
        Task<ShoppingCartDto?> AddProductsToCartAsync(AddToCartRequestDto request);
        Task<bool> RemoveProductFromShoppingCartAsync(int applicationUserId, int productId);
        Task<ShoppingCartDto> GetUserShoppingCartAsync(int applicationUserId, int skip = 0, int take = 10, string sortBy = "title", bool descending = false);
        Task<bool> DeleteCartAsync(int cartId);
        Task<PagedShoppingCartResponseDto?> GetCurrentCartAsync(int userId, int skip = 0, int limit = 1);
    }
}
