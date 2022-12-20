namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель редактирования пользователя.
    /// </summary>
    public class EditUserModel
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Эл. адрес.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Старый пароль.
        /// </summary>
        public string? OldPassword { get; set; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string? NewPassword { get; set; }
        
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Номер моб. телефона.
        /// </summary>
        public string? Mobile { get; set; }
    }
}
