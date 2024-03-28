using System;
using System.Net.Mail;
namespace MailSender
{
    public class SMTPmailSender : IMailSender
    {
        private readonly string smtpHost;
        public SMTPmailSender(string smtpHost)
        {
            this.smtpHost = smtpHost;
        }
        public void Send(MailMessage mailmsg)
        {
            using (SmtpClient smtp = new SmtpClient(smtpHost))
            {
                smtp.Send(mailmsg);
            }
        }
        public static void SendEmail(string toPerson, string subject, string body)
        {
            try
            {
                MailMessage mailmsg = GetMailmessage(toPerson, subject, body);
#if DEBUG
                Console.WriteLine("Send mail to:" + toPerson);
#else
	        //Create a SmtpClient to our smtphost: yoursmtphost
			IMailSender mailSender = new SMTPmailSender("yoursmtphost");
            MailService mailService = new MailService(mailSender);
            mailService.SendEmail(mailmsg);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cant send mail to {0} due to {1}", toPerson, ex.Message);
            }
        }
        public static MailMessage GetMailmessage(string toPerson, string subject, string body) 
        {
            MailMessage mail = new MailMessage { Body = body, From = new MailAddress("info@EO.com"), Subject = subject, IsBodyHtml = true };
            mail.To.Add(toPerson);
            return mail;
        }

    }
}
