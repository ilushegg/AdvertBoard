using AdvertBoard.Domain;

namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class AdvertisementDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Категория.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Изображения.
    /// </summary>
    public List<string> Images { get; set; }

    /// <summary>
    /// Дата создания объявления.
    /// </summary>
    public string DateTimeCreated { get; set; }

    /// <summary>
    /// Локация.
    /// </summary>
    public string LocationQuery { get; set; }

}