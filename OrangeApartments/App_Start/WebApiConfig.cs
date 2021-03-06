﻿using Microsoft.Practices.Unity;
using OrangeApartments.App_Start;
using OrangeApartments.Core;
using OrangeApartments.Core.Repositories;
using OrangeApartments.Migrations;
using OrangeApartments.Persistence;
using OrangeApartments.Persistence.Repository;
using System.Data.Entity;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OrangeApartments
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Enable Cross-Origin-Requests
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // DB initializer
            // Do not remove.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApartmentContext, Configuration>());

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
