
namespace AbySalto.Mid.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(int applicationUserId, int productId);
    }
}
