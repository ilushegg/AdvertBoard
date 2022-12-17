using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Infrastructure.Mail
{
    public class MailService : IMailService
    {

        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateActivationCode()
        {
            try
            {

                var activationCode = Guid.NewGuid().ToString().Substring(0, 10);
                return activationCode;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task SendEmail(string email, string subject, string message)
        {
            if (email != null)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("MIA Board", _configuration["Mail:Email"]));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.RichText)
                {
                    Text = message
                };
                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync("smtp.mail.ru", 465, true);
                        await client.AuthenticateAsync(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                        await client.SendAsync(emailMessage);
                        await client.DisconnectAsync(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }

    }
}
