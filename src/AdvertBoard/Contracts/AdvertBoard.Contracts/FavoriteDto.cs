using System;

namespace AdvertBoard.Contracts;

/// <summary>
/// Модель представления корзины товаров.
/// </summary>
public class FavoriteDto
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
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

}