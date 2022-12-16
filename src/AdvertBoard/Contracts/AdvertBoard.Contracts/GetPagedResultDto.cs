using AdvertBoard.Domain;

namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class GetPagedResultDto<T>
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
    /// Коллекция.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; set; }
}