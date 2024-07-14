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
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Flat")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<bool>("IsLast")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("OnlineShop.Db.Models.Favorite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f12d1d26-f70d-4ac7-a81b-eb95a6c672c5"),
                            ProductId = new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"),
                            Url = "/images/products/f12d1d26-f70d-4ac7-a81b-eb95a6c672c5.jpg"
                        },
                        new
                        {
                            Id = new Guid("499c6682-166c-4983-ae0f-fd769210190f"),
                            ProductId = new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"),
                            Url = "/images/products/499c6682-166c-4983-ae0f-fd769210190f.jpg"
                        },
                        new
                        {
                            Id = new Guid("35aba7da-0380-4ef6-81e2-715765b2c60b"),
                            ProductId = new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"),
                            Url = "/images/products/35aba7da-0380-4ef6-81e2-715765b2c60b.jpg"
                        },
                        new
                        {
                            Id = new Guid("e4f305ee-bbb7-4781-b127-64e079948094"),
                            ProductId = new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"),
                            Url = "/images/products/e4f305ee-bbb7-4781-b127-64e079948094.jpg"
                        },
                        new
                        {
                            Id = new Guid("8e925d1d-7c28-4519-831d-a33ad731338e"),
                            ProductId = new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"),
                            Url = "/images/products/8e925d1d-7c28-4519-831d-a33ad731338e.jpg"
                        },
                        new
                        {
                            Id = new Guid("70bfa4b1-15ec-4165-9180-5137f0c94fd5"),
                            ProductId = new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"),
                            Url = "/images/products/70bfa4b1-15ec-4165-9180-5137f0c94fd5.jpeg"
                        },
                        new
                        {
                            Id = new Guid("555f5555-0fb8-42b2-a683-d129a0540b1e"),
                            ProductId = new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"),
                            Url = "/images/products/555f5555-0fb8-42b2-a683-d129a0540b1e.jpg"
                        },
                        new
                        {
                            Id = new Guid("863e76e0-7dd9-4291-9dcf-d5f48bec0999"),
                            ProductId = new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"),
                            Url = "/images/products/863e76e0-7dd9-4291-9dcf-d5f48bec0999.jpg"
                        },
                        new
                        {
                            Id = new Guid("ce411d49-5599-4aa3-a056-f8591bc65480"),
                            ProductId = new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"),
                            Url = "/images/products/ce411d49-5599-4aa3-a056-f8591bc65480.jpg"
                        },
                        new
                        {
                            Id = new Guid("ebc925ec-6233-4614-8b1f-3fab757a88ac"),
                            ProductId = new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"),
                            Url = "/images/products/ebc925ec-6233-4614-8b1f-3fab757a88ac.jpg"
                        },
                        new
                        {
                            Id = new Guid("a6600881-74b9-4cbe-b2fd-19cdbbe4dadc"),
                            ProductId = new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"),
                            Url = "/images/products/a6600881-74b9-4cbe-b2fd-19cdbbe4dadc.jpg"
                        },
                        new
                        {
                            Id = new Guid("9140d097-a558-4bba-92e1-5f1862480d1d"),
                            ProductId = new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                            Url = "/images/products/9140d097-a558-4bba-92e1-5f1862480d1d.jpg"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentsToCourier")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("StateOfOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"),
                            Cost = 125m,
                            Description = "Очень вкусная лепёшка ver2.0",
                            Name = "Хачапури"
                        },
                        new
                        {
                            Id = new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"),
                            Cost = 300m,
                            Description = "Очень вкусные пельмени",
                            Name = "Хинкали"
                        },
                        new
                        {
                            Id = new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"),
                            Cost = 250m,
                            Description = "Рулетики из виноградных листьев с фаршем внутри",
                            Name = "Долма"
                        },
                        new
                        {
                            Id = new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"),
                            Cost = 200m,
                            Description = "Традиционный татарский десерт",
                            Name = "Чак-чак"
                        },
                        new
                        {
                            Id = new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"),
                            Cost = 90m,
                            Description = "Узбекский пирожок",
                            Name = "Самса"
                        },
                        new
                        {
                            Id = new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"),
                            Cost = 300m,
                            Description = "Вьетнамские фаршированные рулетики",
                            Name = "Нэмы"
                        },
                        new
                        {
                            Id = new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"),
                            Cost = 400m,
                            Description = "Рагу из птицы, тушенной с овощами, зеленью и специями",
                            Name = "Чахохбили"
                        },
                        new
                        {
                            Id = new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"),
                            Cost = 350m,
                            Description = "Вкусный, ароматный, острый",
                            Name = "Суп «Харчо»"
                        },
                        new
                        {
                            Id = new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"),
                            Cost = 250m,
                            Description = "Густой грузинский суп",
                            Name = "Чихиртма"
                        },
                        new
                        {
                            Id = new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"),
                            Cost = 230m,
                            Description = "Армянские тефтели",
                            Name = "Кюфта"
                        },
                        new
                        {
                            Id = new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"),
                            Cost = 450m,
                            Description = "Горячий, очень сытный и жирный суп из говяжьих ног",
                            Name = "Хаш"
                        },
                        new
                        {
                            Id = new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                            Cost = 90m,
                            Description = "Очень вкусная булочка",
                            Name = "Эчпочмак"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favorite", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("UserProductFavorites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.User", "User")
                        .WithMany("UserProductFavorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("Images");

                    b.Navigation("UserProductFavorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");

                    b.Navigation("UserProductFavorites");
                });
#pragma warning restore 612, 618
        }
    }
}
