using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.ShoppingCart.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.Product.Services;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Authorize]
[Route("v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IUserService _userService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public CartController(IShoppingCartService shoppingCartService, IUserService userService)
    {
        _shoppingCartService = shoppingCartService;
        _userService = userService;
    }

    /// <summary>
    /// Возвращает позиции товаров в корзине.
    /// </summary>
    /// <returns>Коллекция элементов <see cref="ShoppingCartDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ShoppingCartDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await _shoppingCartService.GetAsync(cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Обновляет количество товара в корзине.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateQuantityAsync(Guid id, int quantity, CancellationToken cancellationToken)
    {
        await _shoppingCartService.UpdateQuantityAsync(id, quantity, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Создает корзину.
    /// </summary>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrent(cancellationToken);

        var result = await _shoppingCartService.GetAsync(cancellationToken);

        return Ok(result);
    }


    /// <summary>
    /// Удаляет товар из корзины.
    /// </summary>
    /// <param name="id">Идентификатор товара в корзине.</param>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _shoppingCartService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}