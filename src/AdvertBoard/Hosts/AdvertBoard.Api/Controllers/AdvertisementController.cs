using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.ProductImage.Services;
using AdvertBoard.AppServices.Advertisement.Services;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.AppServices.Location.Services;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IUserService _userService;
    private readonly IAdvertisementImageService _productImageService;
    private readonly ICategoryService _categoryService;
    private readonly ILocationService _locationService;

    public AdvertisementController(IAdvertisementService advertisementService, IUserService userService, IAdvertisementImageService productImageService, ILocationService locationService)
    {
        _advertisementService = advertisementService;
        _userService = userService;
        _productImageService = productImageService;
        _locationService = locationService;
    }

    /// <summary>
    /// Возвращает товар по идентификатору.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellation)
    {
        try
        {
            var result = await _advertisementService.GetById(id, cancellation);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Возвращает все товары.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAll([FromQuery]PaginationDto paginationDto, CancellationToken cancellation)
    {
        var result = await _advertisementService.GetAll(paginationDto.Limit, paginationDto.Offset, cancellation);

        return Ok(result);
    }

    /// <summary>
    /// Добавляет товар.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Add([FromBody]AddProductModel model, CancellationToken cancellationToken)
    {
        try
        {
            var user = _userService.GetCurrent(cancellationToken);
            var location = _locationService.Add(model.Country, model.City, model.Street, model.Number, cancellationToken);
            var result = _advertisementService.Add(model.Name, model.Description, model.Price, model.CategoryId, location, user.Result, cancellationToken);
            if(model.Images != null)
            {
                _productImageService.AddAsync(result, model.Images, cancellationToken);
            }
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
        var result = await _advertisementService.EditAsync(productId, name, description, price, categoryId, cancellationToken);
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
        try
        {
            var result = await _advertisementService.DeleteAsync(productId, cancellation);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}