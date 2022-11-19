using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.ProductImage.Services;

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
    private readonly IProductImageService _productImageService;

    public ProductController(IProductService productService, IUserService userService, IProductImageService productImageService)
    {
        _productService = productService;
        _userService = userService;
        _productImageService = productImageService;
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
    public async Task<IActionResult> GetAll([FromQuery]PaginationDto paginationDto, CancellationToken cancellation)
    {
        var result = await _productService.GetAll(paginationDto.Limit, paginationDto.Offset, cancellation);

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
    public async Task<IActionResult> Add(string name, string description, decimal price, string category, IFormFile file, CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrent(cancellationToken);
       
        var result = await _productService.AddAsync(name, description, price, category, user, cancellationToken);
        await _productImageService.AddAsync(result, file, cancellationToken);
        return Created("", new { });
    }

    /// <summary>
    /// Редактирует товар.
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit(Guid productId, string name, string description, decimal price, Guid categoryId, Guid imageId, IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _productService.EditAsync(productId, name, description, price, categoryId, cancellationToken);
        await _productImageService.EditAsync(imageId, file, cancellationToken);
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