

namespace AbySalto.Mid.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IFavoriteService FavoriteService { get; }
        IProductService ProductService { get; }
        IShoppingCartService ShoppingCartService { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
