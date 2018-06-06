using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data_Access_Layer.EF;
using Data_Access_Layer.Entities;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    public class NavController : Controller
    {
        

        public PartialViewResult Menu(string category = null)
        {
            UserContext context = new UserContext();
            var games = context.Games.Include(nameof(Category));
            IEnumerable<string> game = games
                .Select(item => item.Category.CategoryName)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(game);
        }
    }
}