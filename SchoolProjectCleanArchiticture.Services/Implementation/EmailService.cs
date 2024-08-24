using MailKit.Net.Smtp;
using MimeKit;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(EmailSettings emaiSettings)
        {
            _emailSettings=emaiSettings;
        }
        public async Task<string> SendEmail(string Email, string Message,string ? reason)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.portNumber, true);
                await client.AuthenticateAsync(_emailSettings.FromAddress, _emailSettings.Password);
                var bodyBuilder = new BodyBuilder()
                {

                    HtmlBody = $"{Message}",
                    TextBody = "welcome Test"
                };
                var message = new MimeMessage()
                {
                    Body = bodyBuilder.ToMessageBody()

                };
                message.From.Add(new MailboxAddress("Kaoud", _emailSettings.FromAddress));
                message.To.Add(new MailboxAddress("testing", Email));
                message.Subject = reason==null?"new Submitted data":reason;
             var result=  await  client.SendAsync(message);
               await  client.DisconnectAsync(true);
                if (result != null)
                {
                    return "Success";
                }
                return "FaildSendingMessage";
               
            }
        }
    }
}
