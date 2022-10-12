using Microsoft.EntityFrameworkCore;
using AdvertBoard.DataAccess.EntityConfigurations.Product;
using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;

namespace AdvertBoard.DataAccess;

/// <summary>
/// Контекст БД
/// </summary>
public class AdvertBoardContext : DbContext
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
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
    }
}