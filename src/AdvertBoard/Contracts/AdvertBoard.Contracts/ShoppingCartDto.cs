using System;

namespace AdvertBoard.Contracts;

/// <summary>
/// Модель представления корзины товаров.
/// </summary>
public class ShoppingCartDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование товара.
    /// </summary>
    public string ProductName { get; set; }
    
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
}