using AppService_MaktabShop.Domain.AppService.AppServices;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.CategoryAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.OrderItemAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.ProductAgg.Contracts.ServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.AppServiceContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.RepositoryContract;
using Core_MaktabShop.Domain.Core.UserAgg.Contracts.ServiceContract;
using MaktabShop.Infra.Repo.EFCore.Repositories;
using MaktabShop.Infra.SqlServer.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Service_MaktabShop.Domain.Service.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();


builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderItemAppService, OrderItemAppService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();


builder.Services.AddControllersWithViews();

// --- Session (for Cart / Login) ---
builder.Services.AddHttpContextAccessor();

// Session setup
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


WebApplication app = builder.Build();

// --- Middleware pipeline ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// session must be before MVC endpoints
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
