using System.Net.Mail;
namespace MailSender
{
    internal class MailService
    {
        private readonly IMailSender mailSender;
        public MailService(IMailSender mailSender)
        {
            this.mailSender = mailSender;
        }
        public void SendEmail(MailMessage msg)
        {
            mailSender.Send(msg);
        }
    }
}
