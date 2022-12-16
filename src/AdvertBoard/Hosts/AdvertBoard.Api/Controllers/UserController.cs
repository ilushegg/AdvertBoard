
using Microsoft.AspNetCore.Mvc;

using AdvertBoard.Contracts;

using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.Api.Models;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Identity;

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
        try
        {
            var user = await _userService.Register(model.Name, model.Email, model.Password, cancellationToken);

            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Login(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        try { 
            var result = await _userService.Login(userDto, cancellationToken);
            return Ok(new
            {
                accessToken = result.token,
                id = result.userId
            });
            }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("edit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit(EditUserModel model, CancellationToken cancellationToken)
    {
        var user = await _userService.EditAsync(model.Id, model.Name, model.Mobile, cancellationToken);


        return Ok(user);
    }

    [HttpPost("edit_avatar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAvatarAsync(AddUserAvatarModel model, CancellationToken cancellationToken)
    {
        try
        {
            var avatar = _userAvatarService.GetAvatarByUserId(model.UserId);
            if (avatar == Guid.Empty)
            {
                avatar = await _userAvatarService.AddAsync(model.UserId, model.ImageId, cancellationToken);

            }
            else
            {
                avatar = await _userAvatarService.EditAsync(model.UserId, model.ImageId, cancellationToken);
            }


            return Ok(avatar);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

}