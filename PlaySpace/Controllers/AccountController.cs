using PlaySpace.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PlaySpace.Controllers
{
    public class AccountController : Controller
    {
        UserContext context = new UserContext();
        public ActionResult Index()
        {
            var model = context.Users.FirstOrDefault(m=>m.Login == User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                User dbEntry = context.Users.FirstOrDefault(m => m.Login == User.Identity.Name);
                dbEntry.Login = user.Login;
                dbEntry.Password = user.Password;
                dbEntry.Email = user.Email;
                dbEntry.Age = user.Age;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


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
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
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
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if(user == null)
                {
                    //создание нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new User { Login = model.Login, Password = model.Password, Age = model.Age, RoleId = 2, Email = model.Email});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    }
                    //если пользователь удачно добавлен в бд
                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
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