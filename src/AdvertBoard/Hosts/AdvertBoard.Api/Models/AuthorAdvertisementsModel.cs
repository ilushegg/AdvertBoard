namespace AdvertBoard.Contracts
{
    /// <summary>
    /// Модель объявлений автора.
    /// </summary>
    public class AuthorAdvertisementsModel
    {
        /// <summary>
        /// Смещение.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Лимит.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Идентификатор автора.
        /// </summary>
        public Guid AuthorId { get; set; }

        
    }
}
