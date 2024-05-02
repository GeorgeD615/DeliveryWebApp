﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Db.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Flat")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLast")
                        .HasColumnType("bit");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentsToCourier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateOfOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"),
                            Cost = 125m,
                            Description = "Очень вкусная лепёшка ver2.0",
                            ImagePath = "/images/products/hachapury.jpg",
                            Name = "Хачапури"
                        },
                        new
                        {
                            Id = new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"),
                            Cost = 300m,
                            Description = "Очень вкусные пельмени",
                            ImagePath = "/images/products/khinkali.jpg",
                            Name = "Хинкали"
                        },
                        new
                        {
                            Id = new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"),
                            Cost = 250m,
                            Description = "Рулетики из виноградных листьев с фаршем внутри",
                            ImagePath = "/images/products/dolma.jpg",
                            Name = "Долма"
                        },
                        new
                        {
                            Id = new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"),
                            Cost = 200m,
                            Description = "Традиционный татарский десерт",
                            ImagePath = "/images/products/chuck-chuck.jpg",
                            Name = "Чак-чак"
                        },
                        new
                        {
                            Id = new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"),
                            Cost = 90m,
                            Description = "Узбекский пирожок",
                            ImagePath = "/images/products/samsa.jpg",
                            Name = "Самса"
                        },
                        new
                        {
                            Id = new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"),
                            Cost = 300m,
                            Description = "Вьетнамские фаршированные рулетики",
                            ImagePath = "/images/products/nams.jpeg",
                            Name = "Нэмы"
                        },
                        new
                        {
                            Id = new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"),
                            Cost = 400m,
                            Description = "Рагу из птицы, тушенной с овощами, зеленью и специями",
                            ImagePath = "/images/products/chahohbili.jpg",
                            Name = "Чахохбили"
                        },
                        new
                        {
                            Id = new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"),
                            Cost = 350m,
                            Description = "Вкусный, ароматный, острый",
                            ImagePath = "/images/products/soup_harcho.jpg",
                            Name = "Суп «Харчо»"
                        },
                        new
                        {
                            Id = new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"),
                            Cost = 250m,
                            Description = "Густой грузинский суп",
                            ImagePath = "/images/products/chihirtma.jpg",
                            Name = "Чихиртма"
                        },
                        new
                        {
                            Id = new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"),
                            Cost = 230m,
                            Description = "Армянские тефтели",
                            ImagePath = "/images/products/kufta.jpg",
                            Name = "Кюфта"
                        },
                        new
                        {
                            Id = new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"),
                            Cost = 450m,
                            Description = "Горячий, очень сытный и жирный суп из говяжьих ног",
                            ImagePath = "/images/products/hash.jpg",
                            Name = "Хаш"
                        },
                        new
                        {
                            Id = new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                            Cost = 90m,
                            Description = "Очень вкусная булочка ver2.0",
                            ImagePath = "/images/products/eachpochmak.jpg",
                            Name = "Эчпочмак"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("02e79f7a-7d83-4176-9ed5-7d17d61118f4"),
                            Name = "admin"
                        },
                        new
                        {
                            Id = new Guid("492acbaa-5b43-4d86-b7fa-915be0499978"),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("186ac130-d279-4385-8d22-1954eaec2680"),
                            Login = "george",
                            Password = "123456",
                            RoleId = new Guid("02e79f7a-7d83-4176-9ed5-7d17d61118f4")
                        });
                });

            modelBuilder.Entity("ProductUser", b =>
                {
                    b.Property<Guid>("FavoritesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FavoritesId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavoritesId", "FavoritesId1");

                    b.HasIndex("FavoritesId1");

                    b.ToTable("ProductUser");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Address", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("OnlineShop.Db.Models.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProductUser", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("FavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FavoritesId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
