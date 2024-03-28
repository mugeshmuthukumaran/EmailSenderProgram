using BusinessAccess;
using System;
namespace EmailSenderProgram
{
    internal class Program
    {
        /// <summary>
        /// This application is run everyday
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            string Welcomemail = "Send Welcomemail";
            String Comebackmail = "Send Comebackmail";
            String Voucher = "EOComebackToUs";
            bool ExistingCustomerssendmail = true;
            //Call the method that do the work for me, I.E. sending the mails
            Console.WriteLine(Welcomemail);
            bool Newcustomersssendmail = MailServiceProvider.DoWelcomeMail();
#if DEBUG
            //Debug mode, always send Comeback mail
            Console.WriteLine(Comebackmail);
            ExistingCustomerssendmail = MailServiceProvider.DoComebackMail(Voucher);
#else
			//Every Monday run Comeback mail
			if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
			{
				Console.WriteLine(Comebackmail);
				ExistingCustomerssendmail = DoEmailWork2(Voucher);
			}
#endif
            //Check if the sending went OK
            if (Newcustomersssendmail && ExistingCustomerssendmail)
            {
                Console.WriteLine("All mails are sent, I hope...");
            }
            //Check if the sending was not going well...
            else
            {
                Console.WriteLine("Oops, something went wrong when sending mail (I think...)");
            }
            Console.ReadKey();
        }
    }
}
