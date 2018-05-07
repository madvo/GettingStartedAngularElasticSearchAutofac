
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PartsTest.Repositories;
using PartsTest.DTO;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using PartsTest.Controllers;

namespace PartsTest.Autofac
{
    public static class AutofacConfig
    {

        public static void Run()
        {
            SetAutofac();
        }
        private static void SetAutofac()
        {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers
            
            //Register any other components required by your code
            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IBaseRepository<,>)).InstancePerRequest();
      
            //builder.RegisterType<ComponentController>().InstancePerRequest();
            //builder.RegisterType<ComponentAPIController>().InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //Set the MVC DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

        }

    }
}