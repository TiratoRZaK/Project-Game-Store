using PlaySpace.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PlaySpace.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("List", "Games");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем не существует!");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                }
                if(user == null)
                {
                    //создание нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new Models.User { Email = model.Name, Password = model.Password, Age = model.Age, RoleId = 2});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    //если пользователь удачно добавлен в бд
                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("List", "Games");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~");
        }
    }
}