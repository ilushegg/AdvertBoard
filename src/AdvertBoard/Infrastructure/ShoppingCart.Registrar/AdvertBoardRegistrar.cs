using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.AppServices.Services;
using AdvertBoard.DataAccess;
using AdvertBoard.DataAccess.EntityConfigurations.Product;
using AdvertBoard.DataAccess.Interfaces;
using AdvertBoard.Infrastructure.Repository;
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
        
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}