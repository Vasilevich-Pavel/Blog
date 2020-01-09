using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				constraints: new
				{
					controller = "^Home$|^Account$|^Articles$|^NewArticle&|^Categories$",
					action = "^Index$|^Search$|^Contact$|^Login$|^Register$|^Create$|^Admin$|^RestorePassword$|^Article$|^Delete$|^Edit$|^Tegs$|^Categories$|^Category$",
					httpMethod = new HttpMethodConstraint("GET", "POST")
				}

			);
		}
	}
}
