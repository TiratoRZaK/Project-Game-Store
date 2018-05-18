using PlaySpace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PlaySpace.Controllers
{
    public class GamesController : Controller
    {
        public int pageSize = 10;
        UserContext context = new UserContext();

        public ViewResult List(string category, int page = 1, int sort = 1)
        {
            GameListViewModel model;

            if (sort == 1)
            {
                model = new GameListViewModel
                {
                    Games = context.Games.Include(nameof(Category))
                .Where(p => category == null || p.Category.CategoryName == category)
                .OrderByDescending(Game => (Game.Price / 100 * (100 - Game.Discount)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        context.Games.Count() :
                        context.Games.Where(Game => Game.Category.CategoryName == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
                foreach(Game t in model.Games)
                {
                    if(t.CountKeys <= 0)
                    {
                        model.Games = model.Games.Where(m=>m != t);
                    }
                }
            }
            else
            {
                model = new GameListViewModel
                {
                    Games = context.Games.Include(nameof(Category))
                .Where(p => category == null || p.Category.CategoryName == category)
                .OrderBy(Game => (Game.Price / 100 * (100 - Game.Discount)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        context.Games.Count() :
                        context.Games.Where(Game => Game.Category.CategoryName == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
                foreach (Game t in model.Games)
                {
                    if (t.CountKeys <= 0)
                    {
                        model.Games = model.Games.Where(m => m != t);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Action()
        {
            int max = 0;
            foreach (var g in context.Games)
            {
                if (g.Discount > max) max = g.Discount;
            }
            Game game = context.Games
                .FirstOrDefault(s => s.Discount == max);
            return View(game);
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = context.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
