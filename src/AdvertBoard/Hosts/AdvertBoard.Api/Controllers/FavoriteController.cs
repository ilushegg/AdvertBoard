using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.Advertisement.Services;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.AppServices.Favorite.Services;
using AdvertBoard.Api.Models;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[Authorize]
[Route("v1/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly IAdvertisementService _productService;
    private readonly IUserService _userService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public FavoriteController(IFavoriteService favoriteService, IUserService userService, IAdvertisementService productService)
    {
        _favoriteService = favoriteService;
        _userService = userService;
        _productService = productService;
    }

    /// <summary>
    /// Возвращает позиции товаров в корзине.
    /// </summary>
    /// <returns>Коллекция элементов <see cref="FavoriteDto"/>.</returns>
    [HttpGet("get_all_favorites")]
    [ProducesResponseType(typeof(IReadOnlyCollection<FavoriteDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery]PaginationModel model, CancellationToken cancellationToken)
    {
       

        var result = await _favoriteService.GetAllAsync(model.Limit, model.Offset, (Guid)model.UserId, cancellationToken);
        return Ok(result);
    }
    


    /// <summary>
    /// Добавляет товар в корзину.
    /// </summary>
    /// <param name="cancellationToken"></param>
    [HttpPost("add")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync([FromBody]FavoriteModel model, CancellationToken cancellationToken)
    {
        var result = await _favoriteService.AddAsync(model.AdvertisementId, model.UserId, cancellationToken);

        return Ok(result);
    }


    /// <summary>
    /// Удаляет товар из корзины.
    /// </summary>
    /// <param name="id">Идентификатор товара в корзине.</param>
    [HttpDelete("delete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAsync([FromQuery] FavoriteModel model, CancellationToken cancellationToken)
    {

        var favorite = await _favoriteService.GetByAdvertisementId(model.AdvertisementId, model.UserId, cancellationToken);
        await _favoriteService.DeleteAsync(favorite, cancellationToken);
        return NoContent();
    }


  
}