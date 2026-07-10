using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PortfolioBackend.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {   
            _config = config;
        }

        public async Task SendEmailAsync(
            string toEmail,
            string subject,
            string body,
            string? replyToEmail = null
        )
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                emailSettings["SenderName"],
                emailSettings["SenderEmail"]
            ));

            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            if (!string.IsNullOrWhiteSpace(replyToEmail))
            {
                message.ReplyTo.Add(MailboxAddress.Parse(replyToEmail));
            }

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                emailSettings["SmtpHost"],
                int.Parse(emailSettings["SmtpPort"]!),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["SenderPassword"]
            );

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}