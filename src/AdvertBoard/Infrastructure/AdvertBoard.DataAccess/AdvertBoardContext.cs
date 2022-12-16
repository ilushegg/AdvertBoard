using Microsoft.EntityFrameworkCore;
using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AdvertBoard.Domain;
using AdvertBoard.DataAccess.EntityConfigurations.Category;
using AdvertBoard.DataAccess.EntityConfigurations.Advertisement;
using AdvertBoard.DataAccess.EntityConfigurations.User;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;
using AdvertBoard.DataAccess.EntityConfigurations.Image;
using AdvertBoard.DataAccess.EntityConfigurations.AdvertisementImage;
using AdvertBoard.DataAccess.EntityConfigurations.UserRole;
using AdvertBoard.DataAccess.EntityConfigurations.Comment;

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
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
        modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertisementImageConfiguration());
        modelBuilder.ApplyConfiguration(new UserAvatarConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}