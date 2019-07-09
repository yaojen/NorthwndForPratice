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
using Northwnd.Models;
using System.Reflection;
using System.Web.Http;
using System.Data.Entity;
using Northwnd.Service.Service;
using Northwnd.Service.Interface;

namespace Northwnd.AdminWeb.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            ContainerBuilder builder = new ContainerBuilder();

            #region 註冊web project
            //註冊controller
            builder.RegisterControllers(assembly);
            //註冊api
            builder.RegisterApiControllers(assembly);
            #endregion

            #region 註冊service project
            //註冊service結尾的 物件在Northwnd.Service專案中
            var service = Assembly.Load("Northwnd.Service");
            builder.RegisterAssemblyTypes(service).Where(x => x.Name.EndsWith("Service", StringComparison.Ordinal)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IService<>));
            #endregion

            #region 註冊model project
            var models = Assembly.Load("Northwnd.Models");
            //註冊repository
            //builder.RegisterGeneric(typeof(GenericRepository<>))
            //        .As(typeof(IRepository<>))
            //       .InstancePerRequest();

            //註冊UnitOfWork
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();

            //註冊dbContext
            //string connectionString = ConfigurationManager.ConnectionStrings["NORTHWNDEntities"].ConnectionString;
            builder.RegisterType<NORTHWNDEntities>()
                    //      .WithParameter("connectionString", connectionString)
                    .As<DbContext>()
                    .InstancePerRequest();
            #endregion

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