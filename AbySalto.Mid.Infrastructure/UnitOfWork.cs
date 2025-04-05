using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Infrastructure.Context;

namespace AbySalto.Mid.Infrastructure
{
    public class UnitOfWork(DatabaseContext context, IFavoriteService favoriteService, IProductService productService, IShoppingCartService shoppingCartService) : IUnitOfWork
    {
        public IFavoriteService FavoriteService => favoriteService;

        public IProductService ProductService => productService;

        public IShoppingCartService ShoppingCartService => shoppingCartService;

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
