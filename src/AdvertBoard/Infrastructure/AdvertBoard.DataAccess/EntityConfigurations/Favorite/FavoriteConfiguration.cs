using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;

/// <summary>
/// Конфигурация таблицы ShoppingCarts.
/// </summary>
public class FavoriteConfiguration : IEntityTypeConfiguration<Domain.Favorite>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Favorite> builder)
    {
        builder.ToTable("Favorites");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.HasOne(s => s.User)
            .WithMany(p => p.Favorites);
        
    }
}