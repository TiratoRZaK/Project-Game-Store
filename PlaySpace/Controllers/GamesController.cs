using PlaySpace.Abstract;
using PlaySpace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PlaySpace.Controllers
{
    public class GamesController : Controller
    {
        private IGameRepository repository;
        public GamesController (IGameRepository repo)
        {
            repository = repo;
        }
        public int pageSize = 9;
        UserContext context = new UserContext();

        public ActionResult Index(int Id)
        {
            Game dbEntry = context.Games.Include(nameof(Category)).FirstOrDefault(m=>m.GameId == Id);
            return View(dbEntry);
        }

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
            }
            return View(model);
        }

        public ActionResult Action()
        {
            int max = 0;
            int gameId = 1;
            foreach (var g in repository.GetGameList())
            {
                if (g.Discount > max) { max = g.Discount; gameId = g.GameId; }
            }
            Game game = repository.GetGameList()
                .FirstOrDefault(s => s.GameId == gameId);
            return View(game);  
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = repository.GetGameList()
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
