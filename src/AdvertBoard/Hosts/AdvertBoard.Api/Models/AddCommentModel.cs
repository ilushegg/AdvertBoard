namespace AdvertBoard.Contracts;

/// <summary>
/// Модель добавления комментария.
/// </summary>
public class AddCommentModel
{   
    /// <summary>
    /// Текст комментария.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Guid UserId { get; set; }

}