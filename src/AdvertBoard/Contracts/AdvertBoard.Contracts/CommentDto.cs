using AdvertBoard.Domain;
using AdvertBoard.Infrastructure.Enum;

namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class CommentDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Текст комментария.
    /// </summary>
    public string Text { get; set; }

    public string DateTimeCreated { get; set; }

    public CommentStatus Status { get; set; }

    public Guid UserId { get; set; }

    public string UserAvatar { get; set; }

    public string UserName { get; set; }

    public Guid AdvertisementId { get; set; }

}