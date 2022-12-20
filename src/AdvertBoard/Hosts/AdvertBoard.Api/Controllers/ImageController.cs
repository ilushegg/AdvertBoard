using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.Product.Services;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.ProductImage.Services;
using AdvertBoard.AppServices.Image.Services;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с изображениями.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Добавляет новое изображение.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromForm]IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            var result = new Guid();
            /*foreach (var file in files)
            {*/
                if (file != null)
                {
                    result = await _imageService.AddAsync(file, cancellationToken);

                }
            /*}*/
            return Created("", result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Редактирует изображение.
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditAsync(Guid imageId, IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _imageService.EditAsync(imageId, file, cancellationToken);
        return Ok(result);
    }


    /// <summary>
    /// Удаляет изображение.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid imageId, CancellationToken cancellation)
    {
        try
        {
            await _imageService.DeleteAsync(imageId, cancellation);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}