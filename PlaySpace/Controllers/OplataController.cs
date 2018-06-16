using PlaySpace.Abstract;
using PlaySpace.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using PlaySpace.Entities;
using PlaySpace.EF;
using System.Text;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    [Authorize]
    public class OplataController : Controller
    {

        private IOrderProcessor orderProcessor;
        public OplataController(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }

        UserContext context = new UserContext();

        public ActionResult Index(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            return View(order);
        }

        [HttpGet]
        public ActionResult Paid(int Id)
        {
            Order order = context.Orders.FirstOrDefault(o => o.Id == Id);

            order.DataPay = DateTime.Now;
            order.StatusId = 3;

            context.SaveChanges();
            User dbEntry = context.Users.FirstOrDefault(m => m.Login == User.Identity.Name);
            orderProcessor.ProcessOrder(new ShippingDetails
            {
                Email = dbEntry.Email,
                Name = dbEntry.Login
            }, order);
            return View();
        }

        //[HttpPost]
        //public void Paid(string notification_type, string operation_id, string label, string datetime,
        //decimal amount, decimal withdraw_amount, string sender, string sha1_hash, string currency, bool codepro)
        //{
        //    //string key = "x9MePu487ba1u4QSA3vyMVIJ"; // секретный код
        //    //                                 // проверяем хэш
        //    //string paramString = String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}&{8}",
        //    //    notification_type, operation_id, amount, currency, datetime, sender,
        //    //    codepro.ToString().ToLower(), key, label);
        //    //string paramStringHash1 = GetHash(paramString);
        //    //// создаем класс для сравнения строк
        //    //StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        //    //// если хэши идентичны, добавляем данные о заказе в бд
            
        //    //if (0 == comparer.Compare(paramStringHash1, sha1_hash))
        //    //{
               
        //    //}
        //    //else
        //    //{
        //    //    Order order = context.Orders.FirstOrDefault(o => o.Id == Convert.ToInt32(label));

        //    //    order.DataPay = DateTime.Now;
        //    //    order.StatusId = 2;
        //    //    context.SaveChanges();
        //    //}
        //}

        public string GetHash(string val)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}