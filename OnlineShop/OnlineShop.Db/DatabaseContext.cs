using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(cart => cart.Id);
            modelBuilder.Entity<CartItem>().HasKey(cartItem => cartItem.Id);
            modelBuilder.Entity<Product>().HasKey(product => product.Id);
        }

    }
}
