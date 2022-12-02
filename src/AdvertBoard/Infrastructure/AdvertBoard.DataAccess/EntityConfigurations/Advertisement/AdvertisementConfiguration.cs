using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Advertisement;

/// <summary>
/// Конфигурация таблицы Advertisements.
/// </summary>
public class AdvertisementConfiguration : IEntityTypeConfiguration<Domain.Advertisement>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Advertisement> builder)
    {
        builder.ToTable("Advertisements");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();
        
        builder.Property(b => b.Description).HasMaxLength(2000).IsRequired();

        builder.HasOne(s => s.Category)
            .WithMany(p => p.Advertisements)
            .HasForeignKey(s => s.CategoryId);

        builder.Property(b => b.DateTimeCreated);

        builder.Property(b => b.DateTimePublish);

        builder.Property(b => b.DateTimeUpdated);

        builder.HasMany(p => p.AdvertisementImages)
            .WithOne(s => s.Advertisement)
            .HasForeignKey(s => s.AdvertisementId);

        builder.HasOne(s => s.User)
            .WithMany(p => p.Advertisements)
            .HasForeignKey(s => s.UserId);

        builder.HasOne(l => l.Location)
            .WithOne(a => a.Advertisement);
            
        /*
        builder.HasMany(p => p.ShoppingCarts)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId);*/
    }
}