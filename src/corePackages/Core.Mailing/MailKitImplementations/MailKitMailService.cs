using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Core.Mailing.MailKitImplementations
{
    public class MailKitMailService : IMailService
    {
        private IConfiguration _configuration;
        private readonly MailSettings _mailSettings;

        public MailKitMailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public void SendMail(Mail mail)
        {
            using SmtpClient smtp = new();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port);
            if (_mailSettings.IsAuthenticate) smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            smtp.Send(CreateMimeMessage(mail));
            smtp.Disconnect(true);
        }

        public async Task SendMailAsync(Mail mail)
        {
            using SmtpClient smtp = new();
            await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port);
            if (_mailSettings.IsAuthenticate) await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
            await smtp.SendAsync(CreateMimeMessage(mail));
            await smtp.DisconnectAsync(true);
        }

        private MimeMessage CreateMimeMessage(Mail mail)
        {
            MimeMessage email = new();

            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));

            email.To.Add(new MailboxAddress(mail.ToFullName, mail.ToEmail));

            email.Subject = mail.Subject;

            BodyBuilder bodyBuilder = new()
            {
                TextBody = mail.TextBody,
                HtmlBody = mail.HtmlBody
            };

            if (mail.Attachments != null)
                foreach (MimeEntity? attachment in mail.Attachments)
                    bodyBuilder.Attachments.Add(attachment);

            email.Body = bodyBuilder.ToMessageBody();

            return email;
        }
    }
}
