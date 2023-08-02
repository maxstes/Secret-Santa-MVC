using MimeKit;
using MailKit.Net.Smtp;
using System.Text.Json;
using Secret_Santa_MVC.Email;

namespace Secret_Santa_MVC.Service
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email,string subject,string messageText)
        {
            var emailMessage = new MimeMessage();

            using FileStream openStream = File.OpenRead("Email/Email.json");
            Message? message = 
                await JsonSerializer.DeserializeAsync<Message>(openStream);


            //Sender
            emailMessage.From.Add(new MailboxAddress("Secret Santa",message?.Email));
            emailMessage.To.Add(new MailboxAddress("Player", email));//recipient
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = messageText
            };
            using(var client = new SmtpClient())
            {
                await client.ConnectAsync(message?.Host, 2525, true);
                await client.AuthenticateAsync(message?.Email, message?.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
