namespace AdvertBoard.Contracts;

/// <summary>
/// Модель фильтра товаров.
/// </summary>
public class AdvertisementSearchRequest
{
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Смещение.
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// Лимит.
    /// </summary>
    public int Limit { get; set; }
}