using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; } 
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Favorites
            modelBuilder.Entity<Favorite>().HasKey(f => f.Id);
            modelBuilder.Entity<Favorite>()
                .HasOne(upf => upf.User)
                .WithMany(user => user.UserProductFavorites)
                .HasForeignKey(upf => upf.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Favorite>()
                .HasOne(upf => upf.Product)
                .WithMany(product => product.UserProductFavorites)
                .HasForeignKey(upf => upf.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Address 
            modelBuilder.Entity<Address>().HasKey(address => address.Id);
            modelBuilder.Entity<Address>().Property(address => address.City).
                IsRequired().
                HasMaxLength(30);
            modelBuilder.Entity<Address>().Property(address => address.Street).
                IsRequired().
                HasMaxLength(30);
            modelBuilder.Entity<Address>().Property(address => address.House).
                IsRequired().
                HasMaxLength(7);
            modelBuilder.Entity<Address>().Property(address => address.Flat).
                IsRequired();
            modelBuilder.Entity<Address>().Property(address => address.IsLast).
                IsRequired().
                HasDefaultValue(false);
            modelBuilder.Entity<Address>().
                HasOne(address => address.User).
                WithMany(user => user.Addresses).
                HasForeignKey(address => address.UserId);
            #endregion

            #region Cart
            modelBuilder.Entity<Cart>().HasKey(cart => cart.Id);
            #endregion

            #region CartItem
            modelBuilder.Entity<CartItem>().HasKey(cartItem => cartItem.Id);
            modelBuilder.Entity<CartItem>().
                HasOne(item => item.Product).
                WithMany(product => product.CartItems).
                HasForeignKey(item => item.ProductId);
            modelBuilder.Entity<CartItem>().
                HasOne(item => item.Cart).
                WithMany(cart => cart.Items).
                HasForeignKey(item => item.CartId);
            modelBuilder.Entity<CartItem>().
                HasOne(item => item.Order).
                WithMany(Order => Order.CartItems).
                HasForeignKey(item => item.OrderId);
            #endregion

            #region Product
            modelBuilder.Entity<Product>().HasKey(product => product.Id);
            modelBuilder.Entity<Product>().
                Property(product => product.Name).
                HasMaxLength(30).
                IsRequired();
            modelBuilder.Entity<Product>().
                Property(product => product.Cost).
                HasColumnType("decimal(18,2)").
                IsRequired();
            modelBuilder.Entity<Product>().
                Property(product => product.Description).
                HasMaxLength(1000).
                IsRequired();
            #endregion

            #region User
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().
                Property(user => user.UserName).
                HasMaxLength(25).
                IsRequired();
            #endregion

            #region Role
            modelBuilder.Entity<IdentityRole>().HasKey(role => role.Id);
            modelBuilder.Entity<IdentityRole>().
                Property(role => role.Name).
                HasMaxLength(15).
                IsRequired();
            #endregion

            #region Order
            modelBuilder.Entity<Order>().HasKey(order => order.Id);
            modelBuilder.Entity<Order>().
                HasOne(order => order.Address).
                WithMany(address => address.Orders).
                HasForeignKey(order => order.AddressId);
            modelBuilder.Entity<Order>().
                Property(order => order.CommentsToCourier).
                HasMaxLength(1000).
                IsRequired(false);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().
                HasMany(order => order.CartItems).
                WithOne(item => item.Order).
                HasForeignKey(item => item.OrderId).
                OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region DefaultData
            modelBuilder.Entity<Product>().HasData([
                new Product(){
                    Id = new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"),
                    Name =  "Хачапури",
                    Cost = 125,
                    Description = "Очень вкусная лепёшка ver2.0",
                    ImagePath = "/images/products/hachapury.jpg"
                },
                new Product(){
                    Id = new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"),
                    Name = "Хинкали",
                    Cost = 300,
                    Description = "Очень вкусные пельмени",
                    ImagePath = "/images/products/khinkali.jpg"
                },
                new Product(){
                    Id = new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"),
                    Name = "Долма",
                    Cost = 250,
                    Description = "Рулетики из виноградных листьев с фаршем внутри",
                    ImagePath = "/images/products/dolma.jpg"
                },
                new Product(){
                    Id = new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"),
                    Name = "Чак-чак",
                    Cost = 200,
                    Description = "Традиционный татарский десерт",
                    ImagePath = "/images/products/chuck-chuck.jpg"
                },
                new Product(){
                    Id = new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"),
                    Name = "Самса",
                    Cost = 90,
                    Description = "Узбекский пирожок",
                    ImagePath = "/images/products/samsa.jpg"
                },
                new Product(){
                    Id = new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"),
                    Name = "Нэмы",
                    Cost = 300,
                    Description = "Вьетнамские фаршированные рулетики",
                    ImagePath = "/images/products/nams.jpeg"
                },
                new Product(){
                    Id = new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"),
                    Name = "Чахохбили",
                    Cost = 400,
                    Description = "Рагу из птицы, тушенной с овощами, зеленью и специями",
                    ImagePath = "/images/products/chahohbili.jpg"
                },
                new Product(){
                    Id = new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"),
                    Name = "Суп «Харчо»",
                    Cost = 350,
                    Description = "Вкусный, ароматный, острый",
                    ImagePath = "/images/products/soup_harcho.jpg"
                },
                new Product(){
                    Id = new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"),
                    Name = "Чихиртма",
                    Cost = 250,
                    Description = "Густой грузинский суп",
                    ImagePath = "/images/products/chihirtma.jpg"
                },
                new Product(){
                    Id = new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"),
                    Name = "Кюфта",
                    Cost = 230,
                    Description = "Армянские тефтели",
                    ImagePath = "/images/products/kufta.jpg"
                },
                new Product(){
                    Id = new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"),
                    Name = "Хаш",
                    Cost = 450,
                    Description = "Горячий, очень сытный и жирный суп из говяжьих ног",
                    ImagePath = "/images/products/hash.jpg"
                },
                new Product(){
                    Id = new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                    Name = "Эчпочмак",
                    Cost = 90,
                    Description = "Очень вкусная булочка ver2.0",
                    ImagePath = "/images/products/eachpochmak.jpg"
                }
                ]);
            #endregion
        }

    }
}
