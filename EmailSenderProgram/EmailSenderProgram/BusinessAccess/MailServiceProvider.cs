using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using DataAccess;
using MailSender;

namespace BusinessAccess
{
    public class MailServiceProvider
    {
        /// <summary>
        /// Send Welcome mail
        /// </summary>
        /// <returns></returns>
        public static bool DoWelcomeMail()
        {
            try
            {
                const string subject = "Welcome as a new customer at EO!";
                string body = "Hi {0} <br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EO Team";
                //List all customers
                List<Customer> customers = DataLayer.ListCustomers();
                //If the customer is newly registered, one day back in time
                var customerlist = customers.Where(u => u.CreatedDateTime > DateTime.Now.AddDays(-1));
                //loop through list of new customers
                foreach (var customer in customerlist)
                {
                    //All mails are sent! Success!
                    SMTPmailSender.SendEmail(customer.Email, subject, string.Format(body, customer.Email));
                }
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Please check the SMTP configuration");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Something went wrong :(
                return false;
            }
        }
        /// <summary>
        /// Send Customer ComebackMail
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool DoComebackMail(string voucher)
        {
            try
            {
                const string subject = "We miss you as a customer";
                string body = "Hi {0} <br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for. <br>Voucher: {1} <br><br>Best Regards,<br>EO Team";
                //List all customers 
                List<Customer> customers = DataLayer.ListCustomers();
                //List all orders
                List<Order> orders = DataLayer.ListOrders();
                //loop through list of customers
                // We send mail if customer hasn't put an order
                var CustomerMailids = from customer in customers
                                      join order in orders on customer.Email equals order.CustomerEmail into c
                                      from order in c.DefaultIfEmpty()
                                      where order == null
                                      select customer.Email;
                foreach (var mailid in CustomerMailids)
                {
                    //Create a new MailMessage

                    //Send if customer hasn't put order
                    SMTPmailSender.SendEmail(mailid, subject, string.Format(body, mailid, voucher));
                }
                //All mails are sent! Success!
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Something went wrong :(
                return false;
            }
        }

    }
}
