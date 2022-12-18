namespace AdvertBoard.Contracts;

/// <summary>
/// Товар
/// </summary>
public class AddCategoryModel
{   
    /// <summary>
    /// Идентификатор родителя категории.
    /// </summary>
    public Guid ParentCategory { get; set; }

    /// <summary>
    /// Название новой категории.
    /// </summary>
    public string ChildCategory { get; set; }


}