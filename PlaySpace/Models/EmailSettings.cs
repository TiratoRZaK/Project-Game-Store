using System;
using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web;
using PlaySpace.Abstract;
using PlaySpace.Models;

namespace PlaySpace.Models
{
    public class EmailSettings
    {
        public string MailToAddress = "vip.ra.19992@gmail.com";
        public string MailFromAddress = "vip.ra.1999@gmail.com";
        public bool UseSsl = true;
        public string Username = "vip.ra.1999@gmail.com";
        public string Password = "ruslanruslan132";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                StringBuilder body = new StringBuilder()
                    .AppendLine("Здравствуйте, " + shippingInfo.Name + ". Ваш заказ:")
                    .AppendLine("---");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Game.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c})",
                        line.Quantity, line.Game.Name, subtotal)
                        .AppendLine();
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine()
                    .AppendLine("---");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       shippingInfo.Email,		// Кому
                                       "Заказ доставлен!",		// Тема
                                       body.ToString());            // Тело письма

                foreach (var line in cart.Lines)
                {
                    Attachment a = new Attachment(new MemoryStream(line.Game.File), line.Game.Name + ".torrent");
                    mailMessage.Attachments.Add(a);
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}