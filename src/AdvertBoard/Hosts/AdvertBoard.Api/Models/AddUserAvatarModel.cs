namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель добавления аватара пользователя.
    /// </summary>
    public class AddUserAvatarModel
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор изображения.
        /// </summary>
        public Guid ImageId { get; set; }
    }
}
