using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        UserContext context = new UserContext();

        public ViewResult Index()
        {
            return View(context.Categories);
        }

        public ViewResult Create()
        {
            Category category = context.Categories.Add(new Category());
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                context.SaveCategory(category);
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
            Category deletedCategory = context.DeleteCategory(id);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("Категория \"{0}\" была удалена",
                    deletedCategory.CategoryName);
            }
            return RedirectToAction("Index");
        }
    }
}
