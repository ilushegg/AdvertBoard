using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Comment;

/// <summary>
/// Конфигурация таблицы Images.
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Domain.Comment>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(k => k.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();





    }
}