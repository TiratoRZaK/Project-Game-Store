using PlaySpace.Abstract;
using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    public class OplataController : Controller
    {
        UserContext context = new UserContext();

        public ActionResult Index(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            return View(order);
        }
        [HttpPost]
        public void CompletedOrder(string label)
        {
            Order dbEntry = context.Orders.Find(Convert.ToInt32(label));
            context.Statuses.Add(new Status { Name = "EBAT NAHUI"});
            context.SaveChanges();
        }
    }
}