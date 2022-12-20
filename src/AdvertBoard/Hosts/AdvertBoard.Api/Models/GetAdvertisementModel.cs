namespace AdvertBoard.Contracts;

/// <summary>
/// Модель получения объявления.
/// </summary>
public class GetAdvertisementModel
{   

    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Guid? UserId { get; set; }
}