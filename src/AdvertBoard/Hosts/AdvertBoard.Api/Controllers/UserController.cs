
using Microsoft.AspNetCore.Mvc;

using AdvertBoard.Contracts;

using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.Api.Models;

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
    private readonly IUserAvatarService _userAvatarService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public UserController(IUserService userService, IUserAvatarService userAvatarService)
    {
        _userService = userService;
        _userAvatarService = userAvatarService;
    }

    [HttpGet("get_by_id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetById(id, cancellationToken);

        return Ok(user);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
    {
        var user = _userService.Register(model.Name, model.Email, model.Password, model.Number, cancellationToken);
        if (model.Avatar != null)
        {
            var avatar = await _userAvatarService.AddAsync(user.Result, model.Avatar, cancellationToken);

        }

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

    [HttpPost("edit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Edit(EditUserModel model, CancellationToken cancellationToken)
    {
        var user = await _userService.EditAsync(model.Id, model.Name, model.Mobile, cancellationToken);


        return Ok(user);
    }

}