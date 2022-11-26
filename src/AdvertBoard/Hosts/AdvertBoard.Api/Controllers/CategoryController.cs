using System.Net;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.Product.Services;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с категориями.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;

    }

    /// <summary>
    /// Возвращает категории.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellation)
    {
        try
        {
            var result = await _categoryService.GetAll(cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync([FromQuery]Guid parent, string name, CancellationToken cancellation)
    {
        try
        {
            var result = await _categoryService.AddAsync(parent, name, cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}