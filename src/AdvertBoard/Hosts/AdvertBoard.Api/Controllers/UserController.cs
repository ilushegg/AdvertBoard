
using Microsoft.AspNetCore.Mvc;

using AdvertBoard.Contracts;

using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.User.Services;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с корзиной товаров.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get_by_id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetById(id, cancellationToken));
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        var user = await _userService.Register(userDto, new CancellationToken());
        return Created("", new { });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Login(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var result = await _userService.Login(userDto, cancellationToken);
        return Ok(new
        {
            accessToken = result.token,
            id = result.userId
        });
    }

}