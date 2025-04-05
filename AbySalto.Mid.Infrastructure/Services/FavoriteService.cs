using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Context;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly DatabaseContext _context;

        public FavoriteService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToFavoritesAsync(int applicationUserId, int productId)
        {
            var favorite = new Favorite
            {
                ApplicationUserId = applicationUserId,
                ProductId = productId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
