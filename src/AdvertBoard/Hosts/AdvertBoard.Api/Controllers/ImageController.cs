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
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync(IFormFile[] files, CancellationToken cancellationToken)
    {
        try
        {
            var imageIds = new List<Guid>();
            foreach (var file in files)
            {
                if (file != null)
                {
                    var result = await _imageService.AddAsync(file, cancellationToken);
                    imageIds.Add(result);
                }
            }
            return Created("", imageIds);
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
    public async Task<IActionResult> EditAsync(Guid imageId, IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _imageService.EditAsync(imageId, file, cancellationToken);
        return Ok(result);
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