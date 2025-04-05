using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly DatabaseContext _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(DatabaseContext context, HttpClient httpClient, ILogger<ShoppingCartService> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ShoppingCartDto?> AddProductsToCartAsync(AddToCartRequestDto request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync("https://dummyjson.com/carts/add", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to add products to cart. Status code: {StatusCode}", response.StatusCode);
                    return null;
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                var cart = JsonConvert.DeserializeObject<ShoppingCartDto>(responseJson);

                return cart;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding products to the cart.");
                throw;
            }
        }

        public async Task<bool> DeleteCartAsync(int cartId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://dummyjson.com/carts/{cartId}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to delete cart. Status code {StatusCode}", response.StatusCode);
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while deleting the cart");
                throw;
            }
        }

        public async Task<PagedShoppingCartResponseDto?> GetCurrentCartAsync(int userId, int skip = 0, int limit = 1)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://dummyjson.com/carts/user/{userId}?skip={skip}&limit={limit}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to retrive carts for user. Status code: {StatusCode}", response.StatusCode);
                    return null;
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var pagedCartResponse = JsonConvert.DeserializeObject<PagedShoppingCartResponseDto>(responseJson);

                return pagedCartResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while retriving cart");
                throw;
            }
        }

        public Task<ShoppingCartDto> GetUserShoppingCartAsync(int applicationUserId, int skip = 0, int take = 10, string sortBy = "title", bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProductFromShoppingCartAsync(int applicationUserId, int productId)
        {
            throw new NotImplementedException();
        }

        #region database
        public async Task<bool> AddProductToShoppingCartAsyncDB(int applicationUserId, int productId, int quantity)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    ApplicationUserId = applicationUserId,
                    Items = new List<ShoppingCartItem>()
                };
                _context.ShoppingCarts.Add(shoppingCart);
            }

            var existingShoppingCartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (existingShoppingCartItem != null)
            {
                existingShoppingCartItem.Quantity += quantity;
            }
            else
            {
                shoppingCart.Items.Add(new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }


            return await _context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
