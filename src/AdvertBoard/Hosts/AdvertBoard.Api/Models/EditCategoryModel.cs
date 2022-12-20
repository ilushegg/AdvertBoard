namespace AdvertBoard.Contracts;

/// <summary>
/// Модель редактирования категории.
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