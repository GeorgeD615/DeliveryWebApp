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
            if (databaseContext.UserProductFavorites.
                Any(fav => fav.UserId == user.Id && fav.ProductId == product.Id))
                return;
            var newConnection = new UserProductFavorite { User = user, Product = product };
            databaseContext.UserProductFavorites.Add(newConnection);
            databaseContext.SaveChanges();
        }

        public List<Product> GetByUserId(string userId)
        {
            return databaseContext.UserProductFavorites.
                Include(fav => fav.Product).
                Where(fav => fav.UserId == userId).
                Select(fav => fav.Product).ToList();
        }

        public void Remove(User user, Product product)
        {
            var fav = databaseContext.UserProductFavorites
                .FirstOrDefault(fav => fav.UserId == user.Id && fav.ProductId == product.Id);

            if (fav == null)
                return;

            databaseContext.UserProductFavorites.Remove(fav);
            databaseContext.SaveChanges();
        }
    }
}
