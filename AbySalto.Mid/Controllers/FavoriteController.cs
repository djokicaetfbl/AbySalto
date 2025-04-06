using AbySalto.Mid.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbySalto.Mid.WebApi.Controllers
{
    [Route("api/favorite")]
    public class FavoriteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add-to-favorite/{productId}")]
        [Authorize]
        public async Task<IActionResult> AddToFavorites(int productId)
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.FavoriteService.AddToFavoritesAsync(int.Parse(applicationUserId), 1/*productId*/);
            if (result)
            {
                return Ok("Product added to favorites.");
            }
            return BadRequest("Failed to add product to favorites.");
        }
    }
}
