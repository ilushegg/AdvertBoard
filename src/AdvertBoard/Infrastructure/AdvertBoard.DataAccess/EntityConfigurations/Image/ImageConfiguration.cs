using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class ImageConfiguration : IEntityTypeConfiguration<Domain.Image>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(k => k.Id);

        builder.HasOne(p => p.ProductImage)
            .WithOne(p => p.Image);

        builder.HasOne(p => p.UserAvatar)
            .WithOne(p => p.Image);



        /*       builder.HasMany(s => s.Images)
                   .WithOne(p => p.)
                   .HasPrincipalKey(i => i.ImageId);
       */
    }
}