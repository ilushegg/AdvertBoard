
using Microsoft.AspNetCore.Mvc;

using AdvertBoard.Contracts;

using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.Api.Models;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Identity;
using AdvertBoard.Infrastructure;
using RabbitMQ.Client;
using AdvertBoard.Infrastructure.Mail;
using AdvertBoard.Infrastructure.RabbitMQ;

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
    private readonly IMailService _mailService;
    private readonly IRabbitMQClient _rabbitMQ;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCartService"></param>
    public UserController(IUserService userService, IUserAvatarService userAvatarService, IMailService mailService, IRabbitMQClient rabbitMQ, IConfiguration configuration)
    {
        _userService = userService;
        _userAvatarService = userAvatarService;
        _mailService = mailService;
        _rabbitMQ = rabbitMQ;
        _configuration = configuration;
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

            var user = await _userService.Register(model.Name, model.Email, model.Password, null, cancellationToken);
/*            await _rabbitMQ.send(model.Email + "\t" + activationCode);*/
            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("send_activation_code")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendActivationCode([FromQuery]Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var activationCode = await _mailService.GenerateActivationCode();
            var user = await _userService.EditAsync(userId, null, null, null, null, null, activationCode, null, cancellationToken);
            var userDto = await _userService.GetById(userId, cancellationToken);
            String message = String.Format(
                        "Добро пожаловать в MIA Board!\n" +
                        "Пожалуйста, подтвердите свой электронный адрес перейдя по ссылке: {0}",
            (_configuration["Host:Localhost"] + "/auth/activate/" + userId + "/" + activationCode)
            ); 
            await _mailService.SendEmail(userDto.Email, "Активация", message);
            /*            await _rabbitMQ.send(model.Email + "\t" + activationCode);*/
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Activate([FromBody] ActivateUserModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService.DeleteActivationCodeAsync(model.UserId, model.ActivationCode, cancellationToken);
            return Ok(new JsonResult(result));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("send_recovery_code")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendRecoveryCode([FromQuery] string email, CancellationToken cancellationToken)
    {
        try
        {
            var recoveryCode = await _mailService.GenerateActivationCode();
            var user = await _userService.GetWhere(u => u.Email == email, cancellationToken);
            await _userService.EditAsync(user.Id, null, null, null, null, null, null, recoveryCode, cancellationToken);
            String message = String.Format(
                        "Для восстановления пароля перейдите по ссылке: {0}",
            (_configuration["Host:Localhost"] + "/auth/recovering/" + user.Id + "/" + recoveryCode)
            );
            await _mailService.SendEmail(email, "Восстановление пароля", message);
            /*            await _rabbitMQ.send(model.Email + "\t" + activationCode);*/
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("recover_password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecoverPassword([FromBody]RecoverPasswordModel model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService.RecoverPassword(model.UserId, model.NewPassword, cancellationToken);
            return Ok(new JsonResult(result));

        }
        catch (Exception ex)
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Edit([FromBody]EditUserModel model, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.EditAsync(model.Id, model.Email, model.OldPassword, model.NewPassword, model.Name, model.Mobile, null, null, cancellationToken);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    



    [HttpPost("edit_avatar")]
    [Authorize]
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

    [HttpDelete("delete_by_admin")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery]Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.DeleteAsync(userId, cancellationToken);
            return Ok();

        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }


}