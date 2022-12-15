using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.UserRole;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<Domain.UserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(r => r.UserId);
        builder.Property(r => r.Role);

        builder.HasOne(r => r.User)
            .WithOne(u => u.UserRole)
            .HasForeignKey<Domain.UserRole>(r => r.UserId);






        /*  builder.HasMany(p => p.ShoppingCarts)
              .WithOne(s => s.Product)
              .HasForeignKey(s => s.ProductId);*/
    }
}