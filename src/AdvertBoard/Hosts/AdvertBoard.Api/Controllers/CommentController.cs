using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.ProductImage.Services;
using AdvertBoard.AppServices.Comment.Services;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.AppServices.Location.Services;
using AdvertBoard.AppServices.Favorite.Services;
using System.Threading;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с комментариями к объявлению.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;


    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }


    /// <summary>
    /// Возвращает все комментарии к объявлению.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("get_all_by_advertisement")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByAdvertisement([FromQuery]CommentPaginationModel paginationModel, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetAllByAdvertisement(paginationModel.Offset, paginationModel.Limit, paginationModel.Id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает все комментарии пользователя.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("get_all_by_user")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUser([FromQuery] CommentPaginationModel paginationModel, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetAllByUser(paginationModel.Offset, paginationModel.Limit, paginationModel.Id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Добавляет комментарий.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody]AddCommentModel model, CancellationToken cancellationToken)
    {
        try
        {

            var result = await _commentService.AddAsync(model.UserId, model.AdvertisementId, model.Text, cancellationToken);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Редактирует комментарий.
    /// </summary>
    /// <returns></returns>
    [HttpPut("edit")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditAsync([FromBody]EditCommentModel model, CancellationToken cancellationToken)
    {
        try
        {

            var result = await _commentService.EditAsync(model.Id, model.Text, model.Status, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Удаляет комментарий.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromQuery]Guid Id, CancellationToken cancellation)
    {
        try
        {
            await _commentService.DeleteAsync(Id, cancellation);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

  
    /// <summary>
    /// Администратор может удалить любой комментарий пользователя.
    /// </summary>
    /// <returns></returns>
    [HttpDelete("admin_delete_comment")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAdAsync([FromQuery]Guid commentId, CancellationToken cancellation)
    {
        try
        {

            await _commentService.DeleteAsync(commentId, cancellation);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Администратор может удалить все комментарии пользователя.
    /// </summary>
    /// <returns></returns>
    [HttpDelete("admin_delete_comments")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAdsAsync([FromQuery] Guid userId, CancellationToken cancellation)
    {
        try
        {
            var result = await _commentService.GetAllByUser(0, 2147483647, userId, cancellation);
            foreach (var ad in result.Items)
            {
                await _commentService.DeleteAsync(ad.Id, cancellation);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




}