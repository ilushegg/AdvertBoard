using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.AppServices.Services;

using AdvertBoard.DataAccess;
using AdvertBoard.DataAccess.EntityConfigurations.Product;
using AdvertBoard.DataAccess.Interfaces;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.AppServices.ShoppingCart.Services;
using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;
using AdvertBoard.AppServices;
using AdvertBoard.Infrastructure.Identity;
using AdvertBoard.DataAccess.EntityConfigurations.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using AdvertBoard.AppServices.ProductImage.Services;
using AdvertBoard.AppServices.ProductImage.Repositories;
using AdvertBoard.DataAccess.EntityConfigurations.ProductImage;
using AdvertBoard.Infrastructure.FileService;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;
using AdvertBoard.AppServices.Category.Repositories;

namespace AdvertBoard.Registrar;

public static class AdvertBoardRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.IsEssential = true;
        });

        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IDbContextOptionsConfigurator<AdvertBoardContext>, AdvertBoardContextConfiguration>();
        
        services.AddDbContext<AdvertBoardContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AdvertBoardContext>>()
                .Configure((DbContextOptionsBuilder<AdvertBoardContext>)dbOptions)));

        services.AddScoped((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<AdvertBoardContext>()));
        
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        

        services.AddTransient<IShoppingCartService, ShoppingCartService>();
        services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<IProductImageRepository, ProductImageRepository>();

        services.AddTransient<IUserAvatarService, UserAvatarService>();
        services.AddTransient<IUserAvatarRepository, UserAvatarRepository>();

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();

        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IImageRepository, ImageRepository>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}