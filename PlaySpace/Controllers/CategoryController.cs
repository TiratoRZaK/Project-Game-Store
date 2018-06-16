using PlaySpace.Abstract;
using PlaySpace.Models;
using System;
using PlaySpace.Entities;
using PlaySpace.Interfaces;
using System.Collections.Generic;
using PlaySpace.EF;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private IGameRepository repository;
        private ICategoryRepository repositoryC;
        public CategoryController(IGameRepository repositoryGames, ICategoryRepository repositoryCategories)
        {
            repository = repositoryGames;
            repositoryC = repositoryCategories;
        }

        UserContext context = new UserContext();

        public ViewResult Index()
        {
            return View(repositoryC.GetCategoryList());
        }

        public ViewResult Create()
        {
            Category category = repositoryC.Create(new Category());
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                repositoryC.Create(category);
                repositoryC.Save();
                TempData["message"] = string.Format("Категория \"{0}\" была успешно добавлена", category.CategoryName);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(category);
            }
        }

        public ActionResult Delete(int id)
        {
            Category deletedCategory = repositoryC.Delete(id, repository);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("Категория \"{0}\" была удалена",
                    deletedCategory.CategoryName);
            }
            return RedirectToAction("Index");
        }
    }
}
