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
        private IGameRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IGameRepository repository, IOrderProcessor orderProcessor)
        {
            this.repository = repository;
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
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout( ShippingDetails shippingDetails)
        {
            if (GetCart().Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(GetCart(), shippingDetails);
                GetCart().Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}