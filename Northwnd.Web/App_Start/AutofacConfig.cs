using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Configuration;
using Northwnd.Models.Repository;
using Northwnd.Models.Interface;
using System.Reflection;
using System.Web.Http;


namespace Northwnd.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            ContainerBuilder builder = new ContainerBuilder();

            //註冊controller
            builder.RegisterControllers(assembly);
            //註冊api
            builder.RegisterApiControllers(assembly);

            //註冊service結尾的 物件在Northwnd.Service專案中
            var service = Assembly.Load("Northwnd.Service");
            builder.RegisterAssemblyTypes(service).Where(x => x.Name.EndsWith("Service", StringComparison.Ordinal)).AsImplementedInterfaces();

            //註冊UnitOfWork
            var models = Assembly.Load("Northwnd.Models");

            //建立container
            var container = builder.Build();

            //把容器設定給DependencyResolver
            //mvc
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //webapi
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}