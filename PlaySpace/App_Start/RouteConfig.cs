using System.Web.Mvc;
using System.Web.Routing;

namespace PlaySpace
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Games", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminDefault",
                url: "{controller}/{action}/{gameId}",
                defaults: new { controller = "Admin", action = "Index", gameId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AccountDefault",
                url: "{controller}/{action}/{userName}",
                defaults: new { controller = "Account", action = "Index", userName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OplataDefault",
                url: "{controller}/{action}/{orderId}",
                defaults: new { controller = "Oplata", action = "Index", orderId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ActionDefault",
                url: "{controller}/{action}/{model}",
                defaults: new { controller = "Games", action = "Action", model = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OrderDefault",
                url: "{controller}/{action}/{order}",
                defaults: new { controller = "Admin", action = "OrderCheck", order = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EmailDefault",
                url: "{controller}/{action}/{Id}",
                defaults: new { controller = "Cart", action = "Completed", Id = UrlParameter.Optional }
            );
        }
    }
}
