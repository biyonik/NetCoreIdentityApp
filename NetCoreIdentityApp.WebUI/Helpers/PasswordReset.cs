using System.Net;
using System.Net.Mail;

namespace NetCoreIdentityApp.WebUI.Helpers
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.yandex.com");
            mailMessage.From = new MailAddress("");
            mailMessage.To.Add("");
            mailMessage.Subject = "Identity App Parola Sıfırlama";
            mailMessage.Body = "<h2>Şifrenizi sıfırlamak için lütfen aşağıdaki linke tıklayınız</h2><hr>";
            mailMessage.Body += $"<a href='{link}'>Şifremi Yenile</a>";
            mailMessage.IsBodyHtml = true;
            smtpClient.Port = 465;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential()
            {
                UserName = "",
                Password = ""
            };
            smtpClient.SendMailAsync(mailMessage);
        }
    }
}