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
        // GET: Oplata
        public ActionResult Index(int orderId)
        {
            UserContext context = new UserContext();
            var model = context.Orders.Find(orderId);
            return View(model);
        }
    }
}