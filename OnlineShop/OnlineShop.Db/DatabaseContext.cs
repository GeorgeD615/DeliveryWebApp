using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }

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
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Role>().HasKey(role => role.Id);
            modelBuilder.Entity<Address>().HasKey(address => address.Id);
            modelBuilder.Entity<Order>().HasKey(order => order.Id);

            var adminRole = new Role() { Id = new Guid("02e79f7a-7d83-4176-9ed5-7d17d61118f4"), Name = "admin" };
            var userRole = new Role() { Id = new Guid("492acbaa-5b43-4d86-b7fa-915be0499978"), Name = "user" };

            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<Role>().HasData(userRole);

            var defaultAdmin = new User() { Id = new Guid("186ac130-d279-4385-8d22-1954eaec2680"), 
                Login = "george", 
                Password = "123456", 
                RoleId = new Guid("02e79f7a-7d83-4176-9ed5-7d17d61118f4")
            };


            modelBuilder.Entity<User>().HasData(defaultAdmin);

            modelBuilder.Entity<Order>().HasOne(o => o.User).WithMany(o => o.Orders).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Order>().Property(order => order.CommentsToCourier).IsRequired(false);
        }

    }
}
