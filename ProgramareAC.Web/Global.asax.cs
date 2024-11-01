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

            UnityConfig.RegisterComponents();

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


        //TODO Persisten Session. Verison 4 //This method has been added... Maybe it will work correctly... Must to be tested
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    WriteLog.Common.Debug("Session_Start Event. Start");

        //    SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
        //    string sessionCookieName = sessionStateSection.CookieName;

        //    HttpCookie sessionCookie = new HttpCookie(sessionCookieName);

        //    sessionCookie.Value = Session.SessionID;

        //    sessionCookie.Secure = true;

        //    sessionCookie.Path = "/";
        //    sessionCookie.HttpOnly = true;

        //    WriteLog.Common.Debug("SameSiteMode Before: " + sessionCookie.SameSite);

        //    sessionCookie.SameSite = SameSiteMode.None;
        //   // sessionCookie.Expires = DateTime.Now.AddSeconds(1200);


        //    WriteLog.Common.Debug("Session_Start Event. End");

        //    Response.Cookies.Add(sessionCookie);
        //}



        //// TODO Persisten Session. Verison 3 //This method has been added... Maybe it will work correctly... Must to be tested
        //protected void Application_AcquireRequestState(Object sender, EventArgs e)
        //{
        //    WriteLog.Common.Debug("Application_AcquireRequestState. Start");

        //    // Access sessionStateSection to get session cookie name
        //    SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");

        //    string sessionCookieName = sessionStateSection.CookieName;

        //    // Get the session cookie from the request
        //    HttpCookie sessionCookie = Request.Cookies[sessionCookieName];

        //    if (sessionCookie != null)
        //    {
        //        WriteLog.Common.Debug("Application_AcquireRequestState: SessionCookie.SameSite: Before: " + sessionCookie.SameSite);

        //        // Set SameSite and Secure properties directly
        //        sessionCookie.SameSite = SameSiteMode.None;
        //        sessionCookie.Secure = true;

        //        // Add the updated cookie to the response
        //        Response.Cookies.Remove(sessionCookieName); // Remove the old cookie
        //        Response.Cookies.Add(sessionCookie); // Add the modified cookie

        //        WriteLog.Common.Debug("Application_AcquireRequestState: SessionCookie.SameSite: After: " + sessionCookie.SameSite);
        //    }
        //}



        //TODO Persisten Session Verison 2 //A fost adaugata aceasta metoda... Poate va lucra corect... Must to be tested. Parca lucreaza mai corect...
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            WriteLog.Common.Debug("Application_PostAuthenticateRequest. Start");

            SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");

            string sessionCookieName = sessionStateSection.CookieName;

            if (Request.Cookies[sessionCookieName] != null)
            {
                HttpCookie sessionCookie = Response.Cookies[sessionCookieName];

                WriteLog.Common.Debug("Application_PostAuthenticateRequest: SessionCookie.SameSite: Before: " + sessionCookie.SameSite);

                sessionCookie.SameSite = SameSiteMode.None;

                WriteLog.Common.Debug("Application_PostAuthenticateRequest: SessionCookie.SameSite: After: " + sessionCookie.SameSite);

                sessionCookie.Secure = true;

            }
        }





        // TODO Persisten Session. Version 1 Prima metoda de lucru cu sesiia. Not working as exepted
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");

        //    string sessionCookieName = sessionStateSection.CookieName;

        //    if (Request.Cookies[sessionCookieName] != null)
        //    {
        //        HttpCookie sessionCookie = Response.Cookies[sessionCookieName];

        //        //Set cookies values
        //        sessionCookie.Value = Session.SessionID;

        //        sessionCookie.Secure = true;
        //        sessionCookie.Path = "/";

        //        sessionCookie.HttpOnly = true;
        //        sessionCookie.SameSite = SameSiteMode.None;


        //    }
        //}

    }
}
