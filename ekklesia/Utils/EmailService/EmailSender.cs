using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ekklesia.Utils.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Credentials
            var credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password);

            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.From, _emailSettings.UserName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress(email));

            // Smtp client
            var client = new SmtpClient()
            {
                Port = _emailSettings.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = _emailSettings.Server,
                EnableSsl = true,
                Credentials = credentials
            };

            // Send it...         
            client.Send(mail);

            return Task.CompletedTask;
        }
    }
}
