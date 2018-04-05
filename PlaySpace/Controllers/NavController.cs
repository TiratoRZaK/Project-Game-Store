using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository repository;

        public NavController(IGameRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category = null)
        {
            IEnumerable<string> categories = repository.Games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}