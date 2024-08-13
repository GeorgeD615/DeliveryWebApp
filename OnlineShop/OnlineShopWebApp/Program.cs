using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OnlineShop.Db;
using OnlineShop.Db.Implementations;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
using OnlineShopWebApp.Models.Orders;
using OnlineShopWebApp.ReviewApi;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationName", "OnlineShop");

});

string connection = builder.Configuration.GetConnectionString("online_shop");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connection));

// указываем тип пользователя и роли
builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
})
.AddEntityFrameworkStores<DatabaseContext>();

// настройка cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.LoginPath = "/Authorization/Login";
    options.LogoutPath = "/Authorization/Logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});

builder.Services.AddHttpClient("ReviewApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7274/");
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
builder.Services.AddTransient<IFavoritesRepository, FavoritesDbRepository>();
builder.Services.AddTransient<IAddressesRepository, AddressesDbRepository>();
builder.Services.AddTransient<IImagesRepository, ImagesRepository>();
builder.Services.AddTransient<ImagesProvider>();
builder.Services.AddTransient<ReviewApiClient>();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();


// инициализация администратора
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, rolesManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Orders}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Page}/{numberOfProductsPerPage=10}/{pageNumber=1}");

app.Run();
