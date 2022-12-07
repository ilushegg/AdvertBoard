namespace AdvertBoard.Api.Models
{
    public class AddUserAvatarModel
    {
        public Guid UserId { get; set; }

        public Guid ImageId { get; set; }
    }
}
