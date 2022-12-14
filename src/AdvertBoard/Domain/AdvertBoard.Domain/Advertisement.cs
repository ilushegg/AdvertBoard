namespace AdvertBoard.Domain;

/// <summary>
/// Объявление.
/// </summary>
public class Advertisement
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
    /// Идентификатор категорий.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Категория.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Изображения.
    /// </summary>
    public ICollection<AdvertisementImage>? AdvertisementImages { get; set; }

    /// <summary>
    /// Идентификатор пользователя (автора) продукта.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime DateTimeCreated { get; set; }

    /// <summary>
    /// Дата публикации.
    /// </summary>
    public DateTime DateTimePublish { get; set; }

    /// <summary>
    /// Дата редактирования.
    /// </summary>
    public DateTime DateTimeUpdated { get; set; }

    /// <summary>
    /// Активность объявления.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Идентификатор локации.
    /// </summary>
    public Guid LocationId { get; set; }

    /// <summary>
    /// Локация.
    /// </summary>
    public Location? Location { get; set; }

    /// <summary>
    /// Комментарии.
    /// </summary>
    public ICollection<Comment> Comment { get; set; }

    public ICollection<Favorite> Favorites { get; set; }



}