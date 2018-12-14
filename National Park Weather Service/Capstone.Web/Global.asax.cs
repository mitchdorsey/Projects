using Capstone.Web.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            //12. (Global.asax)      Configure IFilmDAL to map to FilmDAL
            string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;
            kernel.Bind<INPGeekDAL>().To<NPGeekDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
        
    }
}
