using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ekklesia.Utils
{
    public class EmailService : IIdentityMessageService
    {
        private readonly string ORIGIN = "ekklesia.services@gmail.com";
        private readonly string PASSWORD = "abc1234";
        public async Task SendAsync(IdentityMessage message)
        {
            using (var emailMessage = new MailMessage())
            {
                emailMessage.Subject = message.Subject;
                emailMessage.To.Add(message.Destination);
                emailMessage.Body = message.Body;
                emailMessage.From = new MailAddress(ORIGIN);

                //SMTP - Simpple Mail Transport Protocol
                using ( var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(ORIGIN, PASSWORD);

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.Timeout = 20000;
                    await smtp.SendMailAsync(emailMessage);
                }

            }
        }
    }
}
