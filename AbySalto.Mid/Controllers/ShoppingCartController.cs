using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbySalto.Mid.WebApi.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDto request)
        {
            var cart = await _shoppingCartService.AddProductsToCartAsync(request);

            if(cart == null)
            {
                return BadRequest("Failed to add products to cart.");
            }

            return Ok(cart);
        }

        [HttpDelete("delete-cart/{cartId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var result = await _shoppingCartService.DeleteCartAsync(cartId);
            return result ? Ok() : BadRequest("Failed to delete cart");
        }

        [HttpGet("get-cart-by-user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetCartByUser(int userId, int skip = 0, int limit = 1)
        {
            var cart = await _shoppingCartService.GetCurrentCartAsync(userId, skip, limit);
            return cart != null ? Ok(cart) : NotFound("Cart not found");
        }

        #region  // add to Database

        [HttpPost("add-db")]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromQuery] int productId, [FromQuery] int quantity = 1)
        {
            var applicationUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _shoppingCartService.AddProductToShoppingCartAsyncDB(applicationUserId, productId, quantity);
            return result ? Ok() : BadRequest("Failed to add product to cart");
        }

        #endregion
    }
}
