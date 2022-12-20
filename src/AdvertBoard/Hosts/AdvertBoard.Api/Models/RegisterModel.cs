namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель регистрации пользователя.
    /// </summary>
    public class RegisterModel
    {

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Эл. адрес.
        /// </summary>
        public string Email { get; set; }

    }
}
