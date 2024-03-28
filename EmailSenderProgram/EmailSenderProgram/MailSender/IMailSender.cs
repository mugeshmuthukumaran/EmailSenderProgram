using System.Net.Mail;

namespace MailSender
{
    internal interface IMailSender
    {
        void Send( MailMessage message);
    }
}
