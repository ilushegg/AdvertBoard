namespace AdvertBoard.Contracts
{
    /// <summary>
    /// Модель пагинации комментариев.
    /// </summary>
    public class CommentPaginationModel
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
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        
    }
}
