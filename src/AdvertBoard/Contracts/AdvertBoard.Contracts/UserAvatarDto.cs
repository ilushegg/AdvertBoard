
namespace AdvertBoard.Contracts
{
    public class UserAvatarDto
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Guid UserId { get; set; }

        public string FilePath { get; set; }

    }
}
