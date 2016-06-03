using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ClinicalWebApi2.App_Start;

namespace ClinicalWebApi2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static MapperConfiguration MapperConfiguration { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapperConfiguration = AutoMapperConfig.RegisterMappings();
            Application["Configuration"] = MapperConfiguration;
        }
    }
}
