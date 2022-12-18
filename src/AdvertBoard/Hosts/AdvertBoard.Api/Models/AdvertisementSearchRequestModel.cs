namespace AdvertBoard.Contracts;

/// <summary>
/// Модель фильтра товаров.
/// </summary>
public class AdvertisementSearchRequestModel
{
    /// <summary>
    /// Смещение.
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// Лимит.
    /// </summary>
    public int Limit { get; set; }
    /// <summary>
    /// Город поиска.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Категория.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// Цена от.
    /// </summary>
    public decimal? FromPrice { get; set; }

    /// <summary>
    /// Цена до.
    /// </summary>
    public decimal? ToPrice { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Сортировка.
    /// </summary>
    public string Sort { get; set; }

}