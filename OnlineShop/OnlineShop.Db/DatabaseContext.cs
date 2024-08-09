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
        public DbSet<Image> Image { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

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

            #region Image
            modelBuilder.Entity<Image>().
                HasOne(image => image.Product).
                WithMany(product => product.Images).
                HasForeignKey(image => image.ProductId).
                OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Avatar
            modelBuilder.Entity<Avatar>().
                HasOne(avatar => avatar.User).
                WithOne(user => user.Avatar).
                HasForeignKey<Avatar>(avatar => avatar.UserId).
                OnDelete(DeleteBehavior.Cascade);
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
            var productId1 = Guid.Parse("b1747d04-5529-4b07-bd2f-de95656d6a48");
            var productId2 = Guid.Parse("2ad860b1-90b1-41ba-b703-350a4af09585");
            var productId3 = Guid.Parse("1ee09a09-d3fa-464d-a81c-28907cbae5e0");
            var productId4 = Guid.Parse("6844a9af-cf36-42c8-9ab6-694e2d91fbf1");
            var productId5 = Guid.Parse("31b3b010-a18a-4f10-b334-ff29e6376fc4");
            var productId6 = Guid.Parse("50d4c0c8-247b-4725-9dcc-4b7d40d200d9");
            var productId7 = Guid.Parse("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4");
            var productId8 = Guid.Parse("df650d29-4749-430f-b5ae-9a464b402cd8");
            var productId9 = Guid.Parse("9a8e4885-d9e9-4a27-bc0e-444d034b7276");
            var productId10 = Guid.Parse("2ee38d36-d843-4540-a6c1-e76c4c7337f1");
            var productId11 = Guid.Parse("a8126b52-7a71-4604-8b51-8ed1e78fe115");
            var productId12 = Guid.Parse("455ef917-bb65-44bc-856e-36a45f788e26");

            modelBuilder.Entity<Image>().HasData([
                new Image(){
                    Id = Guid.Parse("f12d1d26-f70d-4ac7-a81b-eb95a6c672c5"),
                    Url = "/images/products/f12d1d26-f70d-4ac7-a81b-eb95a6c672c5.jpg",
                    ProductId = productId1
                },
                new Image(){
                    Id = Guid.Parse("499c6682-166c-4983-ae0f-fd769210190f"),
                    Url = "/images/products/499c6682-166c-4983-ae0f-fd769210190f.jpg",
                    ProductId = productId2
                },
                new Image(){
                    Id = Guid.Parse("35aba7da-0380-4ef6-81e2-715765b2c60b"),
                    Url = "/images/products/35aba7da-0380-4ef6-81e2-715765b2c60b.jpg",
                    ProductId = productId3
                },
                new Image(){
                    Id = Guid.Parse("e4f305ee-bbb7-4781-b127-64e079948094"),
                    Url = "/images/products/e4f305ee-bbb7-4781-b127-64e079948094.jpg",
                    ProductId = productId4
                },
                new Image(){
                    Id = Guid.Parse("8e925d1d-7c28-4519-831d-a33ad731338e"),
                    Url = "/images/products/8e925d1d-7c28-4519-831d-a33ad731338e.jpg",
                    ProductId = productId5
                },
                new Image(){
                    Id = Guid.Parse("70bfa4b1-15ec-4165-9180-5137f0c94fd5"),
                    Url = "/images/products/70bfa4b1-15ec-4165-9180-5137f0c94fd5.jpeg",
                    ProductId = productId6
                },
                new Image(){
                    Id = Guid.Parse("555f5555-0fb8-42b2-a683-d129a0540b1e"),
                    Url = "/images/products/555f5555-0fb8-42b2-a683-d129a0540b1e.jpg",
                    ProductId = productId7
                },
                new Image(){
                    Id = Guid.Parse("863e76e0-7dd9-4291-9dcf-d5f48bec0999"),
                    Url = "/images/products/863e76e0-7dd9-4291-9dcf-d5f48bec0999.jpg",
                    ProductId = productId8
                },
                new Image(){
                    Id = Guid.Parse("ce411d49-5599-4aa3-a056-f8591bc65480"),
                    Url = "/images/products/ce411d49-5599-4aa3-a056-f8591bc65480.jpg",
                    ProductId = productId9
                },
                new Image(){
                    Id = Guid.Parse("ebc925ec-6233-4614-8b1f-3fab757a88ac"),
                    Url = "/images/products/ebc925ec-6233-4614-8b1f-3fab757a88ac.jpg",
                    ProductId = productId10
                },
                new Image(){
                    Id = Guid.Parse("a6600881-74b9-4cbe-b2fd-19cdbbe4dadc"),
                    Url = "/images/products/a6600881-74b9-4cbe-b2fd-19cdbbe4dadc.jpg",
                    ProductId = productId11
                },
                new Image(){
                    Id = Guid.Parse("9140d097-a558-4bba-92e1-5f1862480d1d"),
                    Url = "/images/products/9140d097-a558-4bba-92e1-5f1862480d1d.jpg",
                    ProductId = productId12
                }
                ]);

            modelBuilder.Entity<Product>().HasData([
                new Product(){
                    Id = productId1,
                    Name =  "Хачапури",
                    Cost = 125,
                    Description = "Очень вкусная лепёшка ver2.0"
                },
                new Product(){
                    Id = productId2,
                    Name = "Хинкали",
                    Cost = 300,
                    Description = "Очень вкусные пельмени"
                },
                new Product(){
                    Id = productId3,
                    Name = "Долма",
                    Cost = 250,
                    Description = "Рулетики из виноградных листьев с фаршем внутри"
                },
                new Product(){
                    Id = productId4,
                    Name = "Чак-чак",
                    Cost = 200,
                    Description = "Традиционный татарский десерт"
                },
                new Product(){
                    Id = productId5,
                    Name = "Самса",
                    Cost = 90,
                    Description = "Узбекский пирожок"
                },
                new Product(){
                    Id = productId6,
                    Name = "Нэмы",
                    Cost = 300,
                    Description = "Вьетнамские фаршированные рулетики"
                },
                new Product(){
                    Id = productId7,
                    Name = "Чахохбили",
                    Cost = 400,
                    Description = "Рагу из птицы, тушенной с овощами, зеленью и специями"
                },
                new Product(){
                    Id = productId8,
                    Name = "Суп «Харчо»",
                    Cost = 350,
                    Description = "Вкусный, ароматный, острый"
                },
                new Product(){
                    Id = productId9,
                    Name = "Чихиртма",
                    Cost = 250,
                    Description = "Густой грузинский суп"
                },
                new Product(){
                    Id = productId10,
                    Name = "Кюфта",
                    Cost = 230,
                    Description = "Армянские тефтели"
                },
                new Product(){
                    Id = productId11,
                    Name = "Хаш",
                    Cost = 450,
                    Description = "Горячий, очень сытный и жирный суп из говяжьих ног"
                },
                new Product(){
                    Id = productId12,
                    Name = "Эчпочмак",
                    Cost = 90,
                    Description = "Очень вкусная булочка"
                }
                ]);
            #endregion
        }

    }
}
