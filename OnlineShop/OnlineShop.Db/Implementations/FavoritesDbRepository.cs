using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class FavoritesDbRepository : IFavoritesRepository
    {
        private readonly DatabaseContext databaseContext;
        public FavoritesDbRepository(DatabaseContext databaseContext) 
        { 
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(User user, Product product)
        {
            if (await databaseContext.Favorites.
                AnyAsync(fav => fav.UserId == user.Id && fav.ProductId == product.Id))
                return;
            var newFavorite = new Favorite { User = user, Product = product };
            databaseContext.Favorites.Add(newFavorite);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetByUserIdAsync(string userId)
        {
            return await databaseContext.Favorites.
                Include(fav => fav.Product).
                ThenInclude(product => product.Images).
                Where(fav => fav.UserId == userId).
                Select(fav => fav.Product).
                ToListAsync();
        }

        public async Task RemoveAsync(User user, Product product)
        {
            var favorite = await databaseContext.Favorites
                .FirstOrDefaultAsync(fav => fav.UserId == user.Id && fav.ProductId == product.Id);

            if (favorite == null)
                return;

            databaseContext.Favorites.Remove(favorite);
            await databaseContext.SaveChangesAsync();
        }
    }
}
