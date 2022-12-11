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
using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.DataAccess.EntityConfigurations.AdvertisementImage;
using AdvertBoard.Infrastructure.FileService;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;
using AdvertBoard.AppServices.Category.Repositories;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.AppServices.User.Repositories;
using AdvertBoard.AppServices.Advertisement.Services;
using AdvertBoard.DataAccess.EntityConfigurations.Advertisement;
using AdvertBoard.AppServices.Image.Services;
using AdvertBoard.DataAccess.EntityConfigurations.Image;
using AdvertBoard.AppServices.Location.Services;
using AdvertBoard.DataAccess.EntityConfigurations.Location;
using AdvertBoard.AppServices.Location.Repositories;

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
        services.AddTransient<IDbContextOptionsConfigurator<AdvertBoardContext>, AdvertBoardContextConfiguration>();
        
        services.AddDbContext<AdvertBoardContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AdvertBoardContext>>()
                .Configure((DbContextOptionsBuilder<AdvertBoardContext>)dbOptions)), ServiceLifetime.Transient);

        services.AddTransient((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<AdvertBoardContext>()));
        
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        

        services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddTransient<IShoppingCartService, ShoppingCartService>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();

        services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
        services.AddTransient<IAdvertisementService, AdvertisementService>();

        services.AddTransient<IAdvertisementImageRepository, AdvertisementImageRepository>();
        services.AddTransient<IAdvertisementImageService, AdvertisementImageService>();

        services.AddTransient<IUserAvatarRepository, UserAvatarRepository>();
        services.AddTransient<IUserAvatarService, UserAvatarService>();

        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICategoryService, CategoryService>();

        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<IImageService, ImageService>();
        
        services.AddTransient<ILocationRepository, LocationRepository>();
        services.AddTransient<ILocationService, LocationService>();

        services.AddTransient<IFileService, FileService>();

        services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}