using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Domain.User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.Name).HasMaxLength(100);

        builder.Property(b => b.Email).HasMaxLength(50);
        
        builder.Property(b => b.Password).HasMaxLength(100);

        builder.Property(b => b.CreateDate);



        /*  builder.HasMany(p => p.ShoppingCarts)
              .WithOne(s => s.Product)
              .HasForeignKey(s => s.ProductId);*/
    }
}