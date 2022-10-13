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
    public async Task<IActionResult> Register(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _userService.Register(login, password, cancellationToken);
        return Created("", new { });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Login(string login, string password, CancellationToken cancellationToken)
    {
        var token = await _userService.Login(login, password, cancellationToken);
        return Ok(token);
    }

}