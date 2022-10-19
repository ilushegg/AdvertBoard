using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var result = await _productService.AddAsync(name, description, price, categoryId, cancellation);
        return Created("", new { });
    }

    /// <summary>
    /// Редактирует товар.
    /// </summary>
    /// <returns></returns>
    [HttpPut]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid productId, CancellationToken cancellation)
    {
        var result = await _productService.DeleteAsync(productId, cancellation);
        return Ok(result);
    }
}