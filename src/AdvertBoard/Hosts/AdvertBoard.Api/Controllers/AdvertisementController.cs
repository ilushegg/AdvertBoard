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
/// Работа с объявлениями.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IFavoriteService _favoriteService;
    private readonly ILocationService _locationService;
    private readonly IAdvertisementImageService _productImageService;

    public AdvertisementController(IAdvertisementService advertisementService, IFavoriteService favoriteService, IAdvertisementImageService productImageService, ILocationService locationService)
    {
        _advertisementService = advertisementService;
        _favoriteService = favoriteService;
        _productImageService = productImageService;
        _locationService = locationService;
    }

    /// <summary>
    /// Возвращает объявление по идентификатору.
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
            if (model.UserId != null)
            {
                result.isFavorite = await _favoriteService.IsAdvertisementFavorite(model.AdvertisementId, (Guid)model.UserId, cancellationToken);
            }
                return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Возвращает все объявления.
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
            foreach(var res in result.Items)
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
        try
        {
            var result = await _advertisementService.GetAllByAuthor(model.Offset, model.Limit, model.AuthorId, cancellation);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    /// <summary>
    /// Добавляет объявление.
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
    /// Редактирует объявление.
    /// </summary>
    /// <returns></returns>
    [HttpPut("edit")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit([FromBody] EditAdvertisementModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result =  await _advertisementService.EditAsync(model.Id, model.Name, model.Description, model.Price, model.CategoryId, cancellationToken);
            var location = _locationService.Edit(result.locId ,model.Country, model.City, model.Street, model.House, model.Flat, model.LocationQueryString, model.Lat, model.Lon);
            if (model.Images != null)
            {
                await _productImageService.EditAsync(result.adId, model.Images, cancellationToken);
            }
            return Ok(result.adId);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Снять объявление с публикации.
    /// </summary>
    /// <returns></returns>
    [HttpPut("edit_public")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditPublic([FromBody]EditAdvertisementPublicModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.EditPublicAsync(model.AdvertisementId, model.Status, cancellationToken);
            return Ok(result);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }






    /// <summary>
    /// Удаляет объявление.
    /// </summary>
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



    /// <summary>
    /// Поиск объявлений.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchAsync([FromQuery]AdvertisementSearchRequestModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.GetAllBySearch(model.Offset, model.Limit, model.Query, model.CategoryId, model.Location, model.FromPrice, model.ToPrice, model.Sort, cancellationToken);
            if (model.UserId != null)
            {
                foreach (var res in result.Items)
                {
                    res.isFavorite = await _favoriteService.IsAdvertisementFavorite(res.Id, (Guid)model.UserId, cancellationToken);
                }


            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Администратор может удалить любое объявление пользователей.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("admin_delete_ad")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAdAsync([FromQuery]Guid advertisementId, CancellationToken cancellation)
    {
        try
        {

            await _advertisementService.DeleteAsync(advertisementId, cancellation);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Администратор может удалить все объявления пользователя.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("admin_delete_ads")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAdsAsync([FromQuery] Guid userId, CancellationToken cancellation)
    {
        try
        {
            var result = await _advertisementService.GetAllByAuthor(0, 2147483647, userId, cancellation);
            foreach (var ad in result.Items)
            {
                await _advertisementService.DeleteAsync(ad.Id, cancellation);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




}