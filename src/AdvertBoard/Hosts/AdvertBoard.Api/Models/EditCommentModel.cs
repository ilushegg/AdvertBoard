using AdvertBoard.Infrastructure.Enum;

namespace AdvertBoard.Contracts;

/// <summary>
/// Модель редактирования комментария.
/// </summary>
public class EditCommentModel
{
    /// <summary>
    /// Идентификатор комментария.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Текст комментария.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Статус комментария.
    /// </summary>
    public CommentStatus Status { get; set;}

}