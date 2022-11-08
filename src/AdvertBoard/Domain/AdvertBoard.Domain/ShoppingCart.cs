namespace AdvertBoard.Domain;

/// <summary>
/// Корзина товаров.
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Количество.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Сумма.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Товар.
    /// </summary>
/*    public ICollection<Product> Products { get; set; }*/

    public Guid UserId { get; set; }

    public User User { get; set; }
}