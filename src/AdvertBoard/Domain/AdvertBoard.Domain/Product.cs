namespace AdvertBoard.Domain;

/// <summary>
/// Товар
/// </summary>
public class Product
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
    public ICollection<ProductImage>? ProductImages { get; set; }

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
    /// Коллекция элементов корзины.
    /// </summary>
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }


}