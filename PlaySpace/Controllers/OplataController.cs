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
        private IOrderProcessor orderProcessor;
        UserContext context = new UserContext();

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult Index(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            return View(order);
        }
        [HttpPost]
        public ActionResult Index(Order order)
        {
            UserContext context = new UserContext();
            User dbEntry = context.Users.FirstOrDefault(m => m.Login == User.Identity.Name);

            orderProcessor.ProcessOrder(GetCart(), new ShippingDetails
            {
                Email = dbEntry.Email,
                Name = dbEntry.Login
            }, order);

            return RedirectToAction("Completed", "Cart");
            
            
            
            

        }
    }
}