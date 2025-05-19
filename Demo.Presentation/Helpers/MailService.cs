using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;

namespace Demo.Presentation.Helpers
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailSettings> _options;

        public MailService(IOptions<MailSettings> options)
        {
            _options = options;
        }
        public void Send(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(email.To));

            mail.From.Add(new MailboxAddress(_options.Value.Email, _options.Value.DisplayName));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            smtp.Connect(_options.Value.Host,
                _options.Value.Port,
                MailKit.Security.SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_options.Value.Email , _options.Value.Password);

            smtp.Send(mail);

            smtp.Disconnect(true);
        }
    }
}
