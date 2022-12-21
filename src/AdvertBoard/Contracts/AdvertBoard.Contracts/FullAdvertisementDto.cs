namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class FullAdvertisementDto
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
    /// Изображения.
    /// </summary>
    public List<Tuple<Guid, string>> Images { get; set; }
    
    /// <summary>
    /// Категория.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Дата создания объявления.
    /// </summary>
    public string DateTimeCreated { get; set; }

    /// <summary>
    /// Дата обновления объявления.
    /// </summary>
    public string DateTimeUpdated{ get; set; }
    
    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Аватар автора.
    /// </summary>
    public string AuthorAvatar { get; set; }

    /// <summary>
    /// Номер телефона автора.
    /// </summary>
    public string AuthorNumber { get; set; }

    /// <summary>
    /// Дата регистрации автора.
    /// </summary>
    public string AuthorRegisterDate { get; set; }

    /// <summary>
    /// Строка адреса.
    /// </summary>
    public string LocationQueryString { get; set; }

    /// <summary>
    /// Координаты широты.
    /// </summary>
    public string LocationLat { get; set; }

    /// <summary>
    /// Координаты долготы.
    /// </summary>
    public string LocationLon { get; set; }

    /// <summary>
    /// В избранном ли у пользователя?
    /// </summary>
    public bool isFavorite { get; set; }


    /// <summary>
    /// Статус объявления.
    /// </summary>
    public string Status { get; set; }


}