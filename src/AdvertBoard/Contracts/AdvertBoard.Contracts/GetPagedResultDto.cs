using AdvertBoard.Domain;

namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class GetPagedResultDto
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
    /// Общее кол-во объявлений.
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// Объявления
    /// </summary>
    public IReadOnlyCollection<AdvertisementDto> Items { get; set; }
}