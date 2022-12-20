namespace AdvertBoard.Contracts
{
    /// <summary>
    /// Модель объявлений с пагинацией.
    /// </summary>
    public class PaginationModel
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
        /// Идентификатор пользователя.
        /// </summary>
        public Guid? UserId { get; set; }

        
    }
}
