namespace AdvertBoard.Domain;

/// <summary>
/// Корзина товаров.
/// </summary>
public class Favorite
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    /// Объявление.
    /// </summary>
    public Advertisement Advertisement { get; set; }
   
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public User User { get; set; }
}