using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyStagram.Core.Models.Helpers.Email;
using MyStagram.Core.Params;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyStagram.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient emailClient;
        private readonly EmailSettings emailSettings;
        
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
            this.emailClient = new SendGridClient(this.emailSettings.ApiKey);
        }

        public async Task<bool> Send(EmailMessage emailMessage)
        {
            var emailContentParams = new EmailContentParams(emailSettings.Sender, emailMessage.Email);
            
            var email = MailHelper.CreateSingleEmail(emailContentParams.FromAddress, emailContentParams.ToAddress, emailMessage.Subject, emailMessage.Message, emailMessage.Message);

            var response = await emailClient.SendEmailAsync(email);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}