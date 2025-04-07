using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GymManagement.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "defaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
