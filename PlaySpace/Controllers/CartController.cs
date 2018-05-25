using PlaySpace.Abstract;
using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    public class CartController : Controller
    {
        UserContext context = new UserContext();
        private IOrderProcessor orderProcessor;
        public CartController(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }
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
        
        public ActionResult Completed(int Id)
        {
            //User dbEntry = context.Users.FirstOrDefault(m => m.Login == User.Identity.Name);
            Order order = context.Orders.Find(Id);
            //orderProcessor.ProcessOrder(GetCart(), new ShippingDetails
            //{
            //    Email = dbEntry.Email,
            //    Name = dbEntry.Login
            //},order);
            order.StatusId = 3;
            context.SaveChanges();
            return View();
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl,
                TotalQuantity = 0
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(int gameId, string returnUrl)
        {
            Game game = context.Games
                .FirstOrDefault(g => g.GameId == gameId);
            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        {
            Game game = context.Games
                .FirstOrDefault(g => g.GameId == gameId);
            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Order AddOrder(Cart cart)
        {
            UserContext context = new UserContext();
            int allprice = 0;
            foreach (var cartline in cart.Lines)
            {
                if (cartline.Game.Discount != 0)
                {
                    allprice += (int)cartline.Quantity * ((int)cartline.Game.Price - (int)(cartline.Game.Price * Decimal.Divide(cartline.Game.Discount, 100)));
                }
                else allprice += (int)cartline.Quantity * (int)cartline.Game.Price;
            }
            User dbEntry = context.Users.FirstOrDefault(m=>m.Login == User.Identity.Name);
            Order order = new Order
            {
                Data = DateTime.Now,
                StatusId = 1,
                UserId = dbEntry.Id,
                AllPrice = allprice
            };
            context.Orders.Add(order);
            context.SaveChanges();
            foreach (CartLine cartline in cart.Lines)
            {
                context.OrdGames.Add(new OrdGame { GameName = cartline.Game.Name, OrderId = order.Id, Quantity = cartline.Quantity });
                context.SaveChanges();
            }
            context.SaveChanges();
            
            return order;
        }

        public ActionResult Checkout()
        {
            if (GetCart().Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                Order order = AddOrder(GetCart());
                return RedirectToAction("Index", "Oplata", new { orderId = order.Id });
            }
            else
            {
                return View();
            }
        }
    }
}