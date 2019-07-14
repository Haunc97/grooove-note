using MailKit.Net.Smtp;
using MimeKit;

namespace PersonalNotesAPI.Auth
{
    public interface IAuthEmailSenderUtil
    {
        void SendEmail(string userId,string ctoken, string toEmail,string emailName,string fromEmail = "haunc97@gmail.com");
    }
    public class AuthEmailSenderUtil : IAuthEmailSenderUtil
    {
        public void SendEmail(string userId,string ctoken, string toEmail, string emailName, string fromEmail = "haunc97@gmail.com")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Note management system", fromEmail));
            message.To.Add(new MailboxAddress(emailName, toEmail));
            message.Subject = "Password confirmation";
            message.Body = new TextPart("plain")
            {
                Text = "http://localhost:4200/#/email-confirmation?userId="+userId+"&ctoken="+ctoken
            };
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(fromEmail, "haunc10081997");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
