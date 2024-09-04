namespace Shared.Utilities.Services
{
    using Application.Interfaces.IRepositories;
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using System.Net.Mail;

    public class EmailSender: IEmailSenderRepository
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly bool useSsl;
        private readonly string user;
        private readonly string fromAddress;
        private readonly string password;
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration=configuration;

            smtpHost = _configuration.GetValue<string>("EmailSettings:SMTPServerIp");
            smtpPort = Convert.ToInt32(_configuration.GetValue<string>("EmailSettings:SMTPPort"));
            useSsl = Convert.ToBoolean(_configuration.GetValue<string>("EmailSettings:UseSSL"));
            fromAddress = _configuration.GetValue<string>("EmailSettings:AccountEmail");
            user = _configuration.GetValue<string>("EmailSettings:User");
            password = _configuration.GetValue<string>("EmailSettings:PassWord");
        }

        public void SendEmail(string toAddress, string subject, string body, List<string> attachmentPaths, string cc)
        {
            var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = useSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                
            };
           
            mailMessage.To.Add(toAddress);
            mailMessage.CC.Add(cc);          

            foreach (var attachmentPath in attachmentPaths)
            {
                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    var attachment = new Attachment(attachmentPath);
                    mailMessage.Attachments.Add(attachment);
                }
            }

            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("rolandooeinfotec@gmail.com", "jxnd vqsx zmfv dpob");
            smtpClient.Send(mailMessage);
        }
    }
}
