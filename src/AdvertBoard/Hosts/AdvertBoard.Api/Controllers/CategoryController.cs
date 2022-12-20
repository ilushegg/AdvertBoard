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


    /// <summary>
    /// Добавляет категорию.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync([FromBody]AddCategoryModel model, CancellationToken cancellation)
    {
        try
        {
            var result = await _categoryService.AddAsync((Guid)model.ParentCategory, model.ChildCategory, cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    /// <summary>
    /// Удаляет категорию.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteAsync([FromQuery]Guid categoryId, CancellationToken cancellation)
    {
        try
        {
            var result = await _categoryService.DeleteAsync(categoryId, cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Редактирует категорию.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> EditAsync([FromBody]EditCategoryModel model, CancellationToken cancellation)
    {
        try
        {
            var result = await _categoryService.EditAsync(model.CategoryId, model.Name, cancellation);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


}