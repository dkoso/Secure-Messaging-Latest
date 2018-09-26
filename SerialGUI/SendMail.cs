using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SerialGUI
{
    public class SendMail
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string To { private get; set; }
        public string From { private get; set; }
        public string Server { private get; set; }

        

        public SendMail(string to, string from, string server)
        {
            this.To = to;
            this.From = from;
            this.Server = server;
        }
        public void SendEmailMessage()
        {
            MailMessage message = new MailMessage(From, To);

            message.Subject = ($"IP { PingVM.ip} Down!");
            message.Body = "LED # Connectivity Alert";

            SmtpClient client = new SmtpClient(Server);
            // Try SecureString later
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(UserName, Password);

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
    }
}
