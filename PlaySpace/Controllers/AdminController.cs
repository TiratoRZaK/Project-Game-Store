using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Entities;
using Data_Access_Layer.EF;
using PlaySpace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        public ICategoryRepository repositoryC;
        public IGameRepository repository;
        public AdminController(IGameRepository repo, ICategoryRepository repoC)
        {
            repository = repo;
            repositoryC = repoC;
        }
        

        UserContext context = new UserContext();

        public ViewResult Index(int id)
        {
            ViewBag.CatName = repositoryC.GetCategoryList().FirstOrDefault(m => m.Id == id).CategoryName;
            ViewBag.CategoryNewGame = id;
            List<Game> gamelist = new List<Game>();
            foreach(Game game in repository.GetGameList())
            {
                if (game.CategoryId == id)
                {
                    gamelist.Add(game);
                }
            }
            return View(gamelist);
        }

        public ViewResult Edit(int gameId)
        {
            Game game = repository.GetGameList()
                .FirstOrDefault(g => g.GameId == gameId);
            SelectList categories = new SelectList(repositoryC.GetCategoryList(), "Id", "CategoryName");
            ViewBag.Categories = categories;
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }                
                repository.Update(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Name);
                return RedirectToAction("Index", new { id=game.CategoryId });
            }
            else
            {
                return View(game);
            }
        }

        public ViewResult Create(int categoryId)
        {
            Game game = new Game();
            game.CategoryId = categoryId;
            game.CountKeys = 1;
            ViewBag.CategoryNewGame = categoryId;
            ViewBag.CatName = context.Categories.FirstOrDefault(m => m.Id == categoryId).CategoryName;
            return View(game);
        }

        [HttpPost]
        public ActionResult Create(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }
                repository.Create(game);
                repository.Save();
                Game dbEntry = context.Games.Include(nameof(Game.Keys)).FirstOrDefault(m=>m.GameId == game.GameId);
                dbEntry.Keys.Add(new Key { Item = game.ActiveKey, GameId = game.GameId });
                repository.Save();
                TempData["message"] = string.Format("Игра \"{0}\" была сохранена", game.Name);
                return RedirectToAction("Index", new { id = game.CategoryId });
            }
            else
            {
                return View("Create", game);
            }
        }

        public ActionResult Delete(int gameId)
        {
            Game deletedGame = repository.Delete(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedGame.Name);
            }
            return RedirectToAction("Index", new { id = deletedGame.CategoryId });
        }

        public ActionResult OrderCheck()
        {
            return View(context.Orders.Include(nameof(Data_Access_Layer.Entities.User)));
        }

        [HttpPost]
        public ActionResult OrderCheck(Order order)
        {
            return RedirectToAction("Index","Oplata",new { order });
        }
    }
}