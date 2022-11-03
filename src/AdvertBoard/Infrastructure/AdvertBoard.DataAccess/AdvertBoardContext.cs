using Microsoft.EntityFrameworkCore;
using AdvertBoard.DataAccess.EntityConfigurations.Product;
using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AdvertBoard.Domain;

namespace AdvertBoard.DataAccess;

/// <summary>
/// Контекст БД
/// </summary>
public class AdvertBoardContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertBoardContext"/>.
    /// </summary>
    public AdvertBoardContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}