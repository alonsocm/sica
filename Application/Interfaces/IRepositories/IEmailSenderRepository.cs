using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IEmailSenderRepository
    {
        public void SendEmail(string toAddress, string subject, string body, List<string> attachmentPaths, string cc);
        
    }
}
