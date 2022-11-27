using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class ProductImageConfiguration : IEntityTypeConfiguration<Domain.ProductImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(k => k.Id);

 /*       builder.HasMany(s => s.Images)
            .WithOne(p => p.)
            .HasPrincipalKey(i => i.ImageId);
*/
    }
}