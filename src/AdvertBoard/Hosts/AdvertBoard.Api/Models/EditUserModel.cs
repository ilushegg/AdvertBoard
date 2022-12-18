namespace AdvertBoard.Api.Models
{
    public class EditUserModel
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }

        public string? Name { get; set; }

        public string? Mobile { get; set; }
    }
}
