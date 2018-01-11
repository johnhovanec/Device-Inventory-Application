using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DeviceHardwareApp2.DAL;                           // Added to test transient db connection issues
using System.Data.Entity.Infrastructure.Interception;   // Added to test transient db connection issues

namespace DeviceHardwareApp2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //DbInterception.Add(new DeviceInterceptorTransientErrors()); // Added to test transient db connection issues
            //DbInterception.Add(new DeviceInterceptorLogging());         // Added to test transient db connection issues
        }
    }
}
