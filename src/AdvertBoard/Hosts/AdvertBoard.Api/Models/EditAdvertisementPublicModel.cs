namespace AdvertBoard.Contracts;

/// <summary>
/// Модель добавления категории.
/// </summary>
public class EditAdvertisementPublicModel
{   
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    /// Статус объявления (Public/hidden).
    /// </summary>
    public string Status { get; set; }


}