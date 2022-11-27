namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class AddProductModel
{   
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
    public Guid[] Images { get; set; }
}