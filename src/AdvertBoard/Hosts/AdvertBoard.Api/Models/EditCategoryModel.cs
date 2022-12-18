namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class EditCategoryModel
{   
    /// <summary>
    /// Идентификатор родителя категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Название новой категории.
    /// </summary>
    public string Name { get; set; }


}