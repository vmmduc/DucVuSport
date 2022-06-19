using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace DucVuSport.Common
{
    public class SendEmail
    {
        public void Send(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();

            bool enableSSL = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress,""), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Host = smtpHost;
            client.EnableSsl = enableSSL;
            client.Port = 587;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(message);
        }
    }
}