using System.Net;
using System.Net.Mail;

namespace IKEA.PL.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("moaz43700@gmail.com", "mbskrqnahqicancc");
            Client.Send("moaz43700@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
