using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.ShoppingCart.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Domain;

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
    private readonly IProductService _productService;
    private readonly IUserService _userService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public CartController(IShoppingCartService shoppingCartService, IUserService userService, IProductService productService)
    {
        _shoppingCartService = shoppingCartService;
        _userService = userService;
        _productService = productService;
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
        var user = await _userService.GetCurrent(cancellationToken);
        var shoppingCart = await _shoppingCartService.GetByProductId(id, user, cancellationToken);
        await _shoppingCartService.UpdateQuantityAsync(shoppingCart.Id, id, quantity, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Добавляет товар в корзину.
    /// </summary>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var product = await _productService.Get(productId, cancellationToken);
        var user = await _userService.GetCurrent(cancellationToken);
        var result = await _shoppingCartService.AddAsync(product, quantity, user, cancellationToken);

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
        var user = await _userService.GetCurrent(cancellationToken);
        var shoppingCart = await _shoppingCartService.GetByProductId(id, user, cancellationToken);
        await _shoppingCartService.DeleteAsync(shoppingCart.Id, cancellationToken);
        return NoContent();
    }
}