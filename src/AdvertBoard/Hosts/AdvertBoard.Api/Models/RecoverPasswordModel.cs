namespace AdvertBoard.Api.Models
{
    public class RecoverPasswordModel
    {
        public Guid UserId { get; set; }

        public string NewPassword { get; set; }

    }
}
