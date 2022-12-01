using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.AdvertisementImage;

/// <summary>
/// Конфигурация таблицы Advertisementsimages.
/// </summary>
public class AdvertisementImageConfiguration : IEntityTypeConfiguration<Domain.AdvertisementImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.AdvertisementImage> builder)
    {
        builder.ToTable("AdvertisementImages");

        builder.HasKey(k => k.Id);

 /*       builder.HasMany(s => s.Images)
            .WithOne(p => p.)
            .HasPrincipalKey(i => i.ImageId);
*/
    }
}