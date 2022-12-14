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
using AdvertBoard.AppServices.Favorite.Services;
using System.Threading;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IFavoriteService _favoriteService;
    private readonly ILocationService _locationService;
    private readonly IAdvertisementImageService _productImageService;
    private readonly ICategoryService _categoryService;

    public AdvertisementController(IAdvertisementService advertisementService, IFavoriteService favoriteService, IAdvertisementImageService productImageService, ILocationService locationService)
    {
        _advertisementService = advertisementService;
        _favoriteService = favoriteService;
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
    public async Task<IActionResult> GetByIdAsync([FromQuery]GetAdvertisementModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.GetById(model.AdvertisementId, cancellationToken);
            result.isFavorite = await _favoriteService.IsAdvertisementFavorite(model.AdvertisementId, (Guid)model.UserId, cancellationToken);
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
    public async Task<IActionResult> GetAll([FromQuery]PaginationModel paginationModel, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.GetAll(paginationModel.Limit, paginationModel.Offset, cancellationToken);
        if(paginationModel.UserId != null)
        {
            foreach(var res in result)
            {
                res.isFavorite = await _favoriteService.IsAdvertisementFavorite(res.Id, (Guid)paginationModel.UserId, cancellationToken);
            }


        }
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
    public IActionResult Add([FromBody]AddAdvertisementModel model)
    {
        try
        {
            
            var location = _locationService.Add(model.Country, model.City, model.Street, model.House, model.Flat, model.LocationQueryString, model.Lat, model.Lon);
            var result = _advertisementService.Add(model.Name, model.Description, model.Price, model.CategoryId, location, model.UserId);
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
    [HttpPut("edit")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit([FromBody] EditAdvertisementModel model, CancellationToken cancellationToken)
    {
        var result =  await _advertisementService.EditAsync(model.Id, model.Name, model.Description, model.Price, model.CategoryId, cancellationToken);
        var location = _locationService.Edit(result.locId ,model.Country, model.City, model.Street, model.House, model.Flat, model.LocationQueryString, model.Lat, model.Lon);
        if (model.Images != null)
        {
            await _productImageService.EditAsync(result.adId, model.Images, cancellationToken);
        }
        return Ok(result.adId);
    }






    /// <summary>
    /// Удаляет товар.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(Guid Id, CancellationToken cancellation)
    {
        try
        {
            await _advertisementService.DeleteAsync(Id, cancellation);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchAsync([FromQuery]AdvertisementSearchRequestModel model, CancellationToken cancellation)
    {
        try
        {
            var result = await _advertisementService.GetAllBySearch(model.Offset, model.Limit, model.Query, model.CategoryId, model.City, cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }





        }