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
        public void Add(User user, Product product)
        {
            if (databaseContext.Favorites.
                Any(fav => fav.UserId == user.Id && fav.ProductId == product.Id))
                return;
            var newFavorite = new Favorite { User = user, Product = product };
            databaseContext.Favorites.Add(newFavorite);
            databaseContext.SaveChanges();
        }

        public List<Product> GetByUserId(string userId)
        {
            return databaseContext.Favorites.
                Include(fav => fav.Product).
                ThenInclude(product => product.Images).
                Where(fav => fav.UserId == userId).
                Select(fav => fav.Product).
                ToList();
        }

        public void Remove(User user, Product product)
        {
            var favorite = databaseContext.Favorites
                .FirstOrDefault(fav => fav.UserId == user.Id && fav.ProductId == product.Id);

            if (favorite == null)
                return;

            databaseContext.Favorites.Remove(favorite);
            databaseContext.SaveChanges();
        }
    }
}
