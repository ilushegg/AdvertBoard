namespace AdvertBoard.Api.Models
{
    /// <summary>
    /// Модель избранного объявления.
    /// </summary>
    public class FavoriteModel
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AdvertisementId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

    }
}
