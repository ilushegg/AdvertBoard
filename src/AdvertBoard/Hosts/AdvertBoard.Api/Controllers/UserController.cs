using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvertBoard.AppServices.ShoppingCart.Services;
using AdvertBoard.Contracts;
using AdvertBoard.AppServices.Product.Services;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        var user = await _userService.Register(userDto.login, userDto.password, new CancellationToken());
        return Created("", new { });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Login(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var token = await _userService.Login(userDto.login, userDto.password, cancellationToken);
        return Ok(new
        {
            accessToken = token
        });
    }

}