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
    private readonly ILocationService _locationService;
    private readonly IAdvertisementImageService _productImageService;
    private readonly ICategoryService _categoryService;

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
    public async Task<IActionResult> GetAll([FromQuery]PaginationModel paginationDto, CancellationToken cancellation)
    {
        var result = await _advertisementService.GetAll(paginationDto.Limit, paginationDto.Offset, cancellation);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает все объявления пользователя.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("get_all_by_author")]
    [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAllByAuthor([FromQuery] AuthorAdvertisementsModel model, CancellationToken cancellation)
    {
        var result = await _advertisementService.GetAllByAuthor(model.Limit, model.Offset, model.AuthorId, cancellation);

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
            var location = _locationService.Add(model.Country, model.City, model.Street, model.House, model.Flat, model.LocationQueryString, model.Lat, model.Lon, cancellationToken);
            var result = _advertisementService.Add(model.Name, model.Description, model.Price, model.CategoryId, location, user.Result, cancellationToken);
            if(model.Images != null)
            {
                _productImageService.Add(result, model.Images);
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
    public async Task<IActionResult> Edit([FromBody] EditAdvertisementModel model, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.EditAsync(model.Id, model.Name, model.Description, model.Price, model.CategoryId, cancellationToken);
/*        await _productImageService.EditAsync(model.Images, file, cancellationToken);*/
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