namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель восстановление пароля.
    /// </summary>
    public class RecoverPasswordModel
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; }

    }
}
