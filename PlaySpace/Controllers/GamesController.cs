using PlaySpace.Models;
using System.Linq;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    public class GamesController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 9;

        public GamesController(IGameRepository repository)
        {
            this.repository = repository;
        }
        public ViewResult List(string category, int page = 1, int sort = 1)
        {
            GameListViewModel model;
            if (sort == 1)
            {
                model = new GameListViewModel
                {
                    Games = repository.Games
                .Where(p => category == null || p.Category == category)
                .OrderByDescending(Game => (Game.Price / 100 * (100 - Game.Discount)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        repository.Games.Count() :
                        repository.Games.Where(Game => Game.Category == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }
            else
            {
                model = new GameListViewModel
                {
                    Games = repository.Games
                .Where(p => category == null || p.Category == category)
                .OrderBy(Game => (Game.Price / 100 * (100 - Game.Discount)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        repository.Games.Count() :
                        repository.Games.Where(Game => Game.Category == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }
            return View(model);
        }

        public ActionResult Action()
        {
            int max = 0;
            foreach(var g in repository.Games)
            {
                if (g.Discount > max) max = g.Discount;
            }
            Game game = repository.Games
                .FirstOrDefault(s => s.Discount == max);
            return View(game);
        }

        


        public FileContentResult GetImage(int gameId)
        {
            Game game = repository.Games
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

        public ActionResult Discount()
        {
            return PartialView("Discount");
        }
    }
}
