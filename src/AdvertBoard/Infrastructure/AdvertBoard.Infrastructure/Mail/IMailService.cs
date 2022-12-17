namespace AdvertBoard.Infrastructure.Mail
{
    public interface IMailService
    {
        Task<string> GenerateActivationCode();

        Task SendEmail(string email, string subject, string message);
    }
}