using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailsSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("sultansouly@gmail.com", "bmkpaxjvzvwwallu");
            Client.Send("sultansouly@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
