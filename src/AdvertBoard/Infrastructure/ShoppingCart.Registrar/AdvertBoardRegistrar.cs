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

namespace AdvertBoard.Registrar;

public static class AdvertBoardRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IDbContextOptionsConfigurator<AdvertBoardContext>, AdvertBoardContextConfiguration>();
        
        services.AddDbContext<AdvertBoardContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AdvertBoardContext>>()
                .Configure((DbContextOptionsBuilder<AdvertBoardContext>)dbOptions)));

        services.AddScoped((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<AdvertBoardContext>()));
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<IShoppingCartService, ShoppingCartService>();
        services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

        return services;
    }
}