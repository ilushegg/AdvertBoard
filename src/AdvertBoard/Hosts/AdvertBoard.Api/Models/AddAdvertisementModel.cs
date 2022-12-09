namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class AddAdvertisementModel
{   
    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Guid UserId { get; set; }

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
    public Guid[]? Images { get; set; }

    /// <summary>
    /// Страна.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Улица.
    /// </summary>
    public string? Street { get; set; }

    /// <summary>
    /// Дом.
    /// </summary>
    public string? House { get; set; }

    /// <summary>
    /// Квартира.
    /// </summary>
    public string? Flat { get; set; }

    /// <summary>
    /// Строка адреса.
    /// </summary>
    public string LocationQueryString { get; set; }

    /// <summary>
    /// Координаты широты.
    /// </summary>
    public string? Lat { get; set; }

    /// <summary>
    /// Координаты долготы.
    /// </summary>
    public string? Lon { get; set; }
}