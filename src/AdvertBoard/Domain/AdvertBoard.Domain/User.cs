namespace AdvertBoard.Domain;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Эл. адрес.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Аватар.
    /// </summary>
    public UserAvatar Avatar { get; set; }

    /// <summary>
    /// Дата регистрации.
    /// </summary>
    
    public DateTime CreateDate { get; set; }

    public ICollection<Product> Products { get; set; }

    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    
}