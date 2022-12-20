namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель активации пользователя.
    /// </summary>
    public class ActivateUserModel
    {

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Код активации.
        /// </summary>
        public string ActivationCode { get; set; }
    }
}
