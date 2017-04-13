using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Domain.Organization
{

    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "accessoriesstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\accessories";
    }

    public class EmailOrder : IOrder
    {
        private EmailSettings emailSettings;

        public EmailOrder(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Card card, Shiping shiping)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder sb = new StringBuilder()
                    .AppendLine("New order")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var l in card.Cards)
                {
                    var subtotal = l.Accessory.Cost * l.Quantity;
                    sb.AppendFormat("{0} x {1} (Total: {2:c}",
                        l.Quantity, l.Accessory.Name, subtotal);
                }

                sb.AppendFormat("Total cost: {0:c}", card.TotalCost())
                    .AppendLine("---")
                    .AppendLine("Delivery:")
                    .AppendLine(shiping.Name)
                    .AppendLine(shiping.Adress)
                    //.AppendLine(shiping.Date)
                    .AppendLine(shiping.ShippingId)
                    .AppendLine("---");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	
                                       emailSettings.MailToAddress,		
                                       "The order has been send!",		
                                       sb.ToString()); 				

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.HeadersEncoding = Encoding.UTF8;

                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}