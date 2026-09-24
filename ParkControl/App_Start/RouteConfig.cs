using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace ParkControl
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            // Off en lugar de Permanent: el modo Permanent emite redirecciones 301,
            // que los navegadores cachean de forma agresiva y provocan comportamientos
            // dificiles de diagnosticar durante el desarrollo. Las URLs sin extension
            // siguen funcionando igual; lo unico que se desactiva es la redireccion
            // automatica desde la version con .aspx.
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { action = "Index", id = UrlParameter.Optional }
                );  
        }
    }
}
