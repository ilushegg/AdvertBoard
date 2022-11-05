using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.Domain;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public ProductController(IProductService productService, IUserService userService)
    {
        _productService = productService;
        _userService = userService;
    }

    /// <summary>
    /// Возвращает все товары.
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProductDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
    {
        var result = await _productService.GetAll(take, skip, cancellation);

        return Ok(result);
    }

    /// <summary>
    /// Добавляет товар.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(string name, string description, decimal price, string category, CancellationToken cancellation)
    {
        var user = await _userService.GetCurrent(cancellation);
        Console.WriteLine("USER : " + user.Name);
       
        var result = await _productService.AddAsync(name, description, price, category, user, cancellation);
        return Created("", new { });
    }

    /// <summary>
    /// Редактирует товар.
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var result = await _productService.EditAsync(productId, name, description, price, categoryId, cancellation);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет товар.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid productId, CancellationToken cancellation)
    {
        var result = await _productService.DeleteAsync(productId, cancellation);
        return Ok(result);
    }
}