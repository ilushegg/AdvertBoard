using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.ShoppingCart.Services;
using AdvertBoard.Contracts;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public CartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    /// <summary>
    /// Возвращает позиции товаров в корзине.
    /// </summary>
    /// <returns>Коллекция элементов <see cref="ShoppingCartDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ShoppingCartDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _shoppingCartService.GetAsync();
        return Ok(result);
    }
    
    /// <summary>
    /// Обновляет количество товара в корзине.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateQuantityAsync(Guid id, int quantity)
    {
        await _shoppingCartService.UpdateQuantityAsync(id, quantity);
        return NoContent();
    }

    /// <summary>
    /// Удаляет товар из корзины.
    /// </summary>
    /// <param name="id">Идентификатор товара в корзине.</param>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _shoppingCartService.DeleteAsync(id);
        return NoContent();
    }
}