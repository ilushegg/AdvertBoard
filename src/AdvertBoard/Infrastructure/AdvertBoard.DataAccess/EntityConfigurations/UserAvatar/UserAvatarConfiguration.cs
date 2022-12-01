using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class UserAvatarConfiguration : IEntityTypeConfiguration<Domain.UserAvatar>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.UserAvatar> builder)
    {
        builder.ToTable("UserAvatars");

        builder.HasKey(k => k.Id);

/*        builder.HasMany(p => p.Images)
            .WithOne(s => s.UserAvatar);*/
    }
}