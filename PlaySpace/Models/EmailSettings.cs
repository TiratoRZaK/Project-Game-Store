using System.Text;
using System.Net;
using System.Net.Mail;
using PlaySpace.Abstract;
using System.Linq;
using System.Collections.Generic;

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
        IGameRepository repository;

        UserContext context = new UserContext();

        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings, IGameRepository repo)
        {
            emailSettings = settings;
            repository = repo;
        }

        public void ProcessOrder(ShippingDetails shippingInfo, Order order)
        {
            List<OrdGame> ordGame = new List<OrdGame>();
            foreach(OrdGame ord in context.OrdGames)
            {
                if(ord.OrderId == order.Id)
                {
                    ordGame.Add(ord);
                }
            }
            List<CartLine> cartLines = new List<CartLine>();
            int allPrices = 0;
            foreach (OrdGame ord in ordGame)
            {
                Game dbGame = repository.GetGameList().FirstOrDefault(f => f.Name == ord.GameName);
                cartLines.Add(new CartLine { Game = dbGame, Quantity = ord.Quantity });
            }

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

                foreach (var line in cartLines)
                {
                    var subtotal = (line.Game.Price / 100 * (100 - line.Game.Discount)) * line.Quantity;
                    allPrices += (int)subtotal;
                    body.AppendFormat("{0} x {1} (итого: {2:c})",
                        line.Quantity, line.Game.Name, subtotal)
                        .AppendLine();
                }
                
                body.AppendFormat("Общая стоимость: {0:c}", allPrices)
                    .AppendLine()
                    .AppendLine("---");
                
                foreach (var line in cartLines)
                {
                    int i = 0;
                    while (i < line.Quantity)
                    {
                        Game dbEntry = context.Games.Include(nameof(Game.Keys)).FirstOrDefault(g => g.GameId == line.Game.GameId);
                        dbEntry.ActiveKey = context.Keys.FirstOrDefault(p => p.GameId == line.Game.GameId).Item;
                        context.ItemKeys.Add(new ItemKey { Keys = dbEntry.ActiveKey, OrdGameId = context.OrdGames.FirstOrDefault(t=>t.OrderId == order.Id && t.GameName == line.Game.Name).Id});
                        body.AppendFormat("Ваш ключ для игры {0}:{1}.", line.Game.Name, dbEntry.ActiveKey);
                        Key delkey = context.Keys.FirstOrDefault(p => p.Item == dbEntry.ActiveKey);
                        context.Keys.Remove(delkey);
                        context.SaveChanges();
                        i++;
                    }
                    body.AppendLine();
                    
                       
                }
                body.AppendLine("Спасибо за покупку! :)");
                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       shippingInfo.Email,		// Кому
                                       "Заказ доставлен!",		// Тема
                                       body.ToString());            // Тело письма
                smtpClient.Send(mailMessage);
            }
        }
    }
}