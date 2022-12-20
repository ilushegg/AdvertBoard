using Microsoft.AspNetCore.Mvc;
using AdvertBoard.Contracts;
using Microsoft.AspNetCore.Authorization;
using AdvertBoard.AppServices.User.Services;
using AdvertBoard.Api.Models;
using AdvertBoard.Infrastructure.Mail;
using AdvertBoard.Infrastructure.RabbitMQ;

namespace AdvertBoard.Api.Controllers;

/// <summary>
/// Работа с пользователями.
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

    public UserController(IUserService userService, IUserAvatarService userAvatarService, IMailService mailService, IRabbitMQClient rabbitMQ, IConfiguration configuration)
    {
        _userService = userService;
        _userAvatarService = userAvatarService;
        _mailService = mailService;
        _rabbitMQ = rabbitMQ;
        _configuration = configuration;
    }

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get_by_id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetById(id, cancellationToken);

        return Ok(user);
    }


    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Отправить ссылку для активации.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Активировать пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Отправить ссылку для восстановления пароля.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Восстановить пароль пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Войти.
    /// </summary>
    /// <param name="userDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Редактировать пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    
    /// <summary>
    /// Редактировать аватар пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Удалить пользователя (для АДМИНА).
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete_by_admin")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery]Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.DeleteAsync(userId, cancellationToken);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}