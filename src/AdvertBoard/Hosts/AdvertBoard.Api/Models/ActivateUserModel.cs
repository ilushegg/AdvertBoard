namespace AdvertBoard.Api.Models
{
    public class ActivateUserModel
    {
        public Guid UserId { get; set; }

        public string ActivationCode { get; set; }
    }
}
