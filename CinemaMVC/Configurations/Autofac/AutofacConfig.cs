using Autofac;
using Autofac.Integration.Mvc;
using CinemaMVC.Models;
using CinemaMVC.Repositories;
using System.Web.Mvc;

namespace CinemaMVC.Configurations.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<MovieRepository>().As<IRepository<Movie>>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}