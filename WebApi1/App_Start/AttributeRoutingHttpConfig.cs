using System.Web.Http;
using AttributeRouting.Web.Http.WebHost;
using WebApi1.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApi1.AttributeRoutingHttpConfig), "Start")]

namespace WebApi1 
{
    public static class AttributeRoutingHttpConfig
	{
		public static void RegisterRoutes(HttpRouteCollection routes) 
		{    
			// See http://github.com/mccalltd/AttributeRouting/wiki for more options.
			// To debug routes locally using the built in ASP.NET development server, go to /routes.axd

            routes.MapHttpAttributeRoutes();
		}

        public static void Start() 
		{
            GlobalConfiguration.Configuration.Routes.MapHttpAttributeRoutes(
                cfg => {
                    cfg.InMemory = true;
                    cfg.AutoGenerateRouteNames = true;
                    cfg.AddRoutesFromAssemblyOf<ProductController>();
                });
            //RegisterRoutes(GlobalConfiguration.Configuration.Routes);
        }
    }
}
