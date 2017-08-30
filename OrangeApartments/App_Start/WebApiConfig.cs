using Microsoft.Practices.Unity;
using OrangeApartments.App_Start;
using OrangeApartments.Core;
using OrangeApartments.Core.Repositories;
using OrangeApartments.Persistence;
using OrangeApartments.Persistence.Repository;
using System.Net.Http.Headers;
using System.Web.Http;
using OrangeApartments.Filters;

namespace OrangeApartments
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // IoC
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IUserRepository, UserRepository>();
            config.DependencyResolver = new IoC(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
