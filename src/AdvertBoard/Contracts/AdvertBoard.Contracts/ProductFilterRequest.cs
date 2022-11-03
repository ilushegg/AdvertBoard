namespace AdvertBoard.Contracts;

/// <summary>
/// Модель фильтра товаров.
/// </summary>
public class ProductFilterRequest
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }
}