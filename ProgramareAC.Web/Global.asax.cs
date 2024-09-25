using ProgramareAC.Models.LogHelper;
using ProgramareAC.Services.MPASS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProgramareAC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



            InitializeApplication();

            WriteLog.InitLoggers();
        }



        private void InitializeApplication()
        {
            // MPASS
            MPASSConfiguration.InitializeSettingsMVC5();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");

            string sessionCookieName = sessionStateSection.CookieName;

            if (Request.Cookies[sessionCookieName] != null)
            {
                HttpCookie sessionCookie = Response.Cookies[sessionCookieName];

                //Set cookies values
                sessionCookie.Value = Session.SessionID;

                sessionCookie.Secure = true;
                sessionCookie.Path = "/";

                sessionCookie.HttpOnly = true;
                sessionCookie.SameSite = SameSiteMode.None;
            }
        }

    }
}
