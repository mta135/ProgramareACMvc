using ProgramareAC.Models;
using ProgramareAC.Models.LogHelper;
using ProgramareAC.Services.MPASS;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Xml;

namespace ProgramareAC.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult MpassAuthentication()
        {
            WriteLog.Common.Info("AuthenticationController/MpassAuthentication");

            return View();
        }

        #region MPASS

        //#######################    MPASS Methods    ###############################

        private string RequestBaseUrl()
        {
            HttpRequestBase request = HttpContext.Request;
            string url = $"{request.Url.Scheme}s://{request.Url.Authority}{request.ApplicationPath.TrimEnd('/')}";

            return url;
        }

        [HttpGet]
        public ActionResult MPASSLogin()
        {
            var postBackUrl = string.Format("{0}/{2}/{1}", RequestBaseUrl(), "MPASSLoginResponse", "Authentication");
            var authnRequestID = "_" + Guid.NewGuid();

            Session.SetRequestIDSessionKey(authnRequestID);

            WriteLog.Common.Info("Method MPASSLogin. PostBackUrl: " + postBackUrl);

            WriteLog.Common.Info("Method MPASSLogin. AuthnRequestID: " + authnRequestID);

            var authnRequest = SamlHelper.BuildAuthnRequest(authnRequestID, MPASSConfiguration.SamlLoginDestination, postBackUrl, MPASSConfiguration.Issuer);

            // redirect to IdP
            ViewBag.IdPUrl = MPASSConfiguration.SamlLoginDestination;
            ViewBag.SAMLVariable = "SAMLRequest";
            ViewBag.SAMLMessage = SamlHelper.Encode(authnRequest);

            // NOTE: Optional (remove if not needed) RelayState must not exceed 80 bytes in length, as specified by [SAMLBind, 3.5.3]
            ViewBag.RelayState = "eFile AuthnRequest Relay State";

            return View("MPASSRedirect");
        }

        [OutputCache(Duration = 0, Location = OutputCacheLocation.None, NoStore = true)]
        [HttpPost]
        public ActionResult MPASSLoginResponse(string samlResponse, string relayState)
        {
            if (!string.IsNullOrEmpty(samlResponse))
            {
                XmlNamespaceManager ns;

                string sessionIndex;
                string nameID;

                Dictionary<string, IList<string>> attributes;

                var expectedUrl = string.Format("{0}/{2}/{1}", RequestBaseUrl(), "MPASSLoginResponse", "Authentication");

                string RequestIDSessionKey = Session.GetRequestIDSessionKey();

                WriteLog.Common.Info("Method: MPASSLoginResponse. ExpectedUrl: " + expectedUrl);

                WriteLog.Common.Info("Method: MPASSLoginResponse. RequestIDSessionKey: " + RequestIDSessionKey);

                SamlHelper.LoadAndVerifyLoginResponse(samlResponse, expectedUrl, TimeSpan.Parse(MPASSConfiguration.SamlMessageTimeout),
                    RequestIDSessionKey, MPASSConfiguration.Issuer, out ns, out sessionIndex, out nameID, out attributes, out bool isAuthenticationCancelled);

                if (isAuthenticationCancelled)
                {
                    // string text = "A apraut o eroare la foramrea autentificarii..."
                    return RedirectToAction("Login");
                }

                // remove RequestID from session to stop replay attacks
                Session.ClearRequestIDSessionKey();

                // save SessionIndex in session
                Session.SetSessionIndexSessionKey(sessionIndex);

                if (!string.IsNullOrEmpty(nameID))
                {
                    SamlHelper.SAMLUserData samlUserData = SamlHelper.CreateUserData(nameID, sessionIndex, attributes);

                    FormsAuthentication.SetAuthCookie("MpassAuthentication", false);

                    Session.SetLoginTypeKey("MPASS");

                    Session.SetSessionUser(samlUserData);

                    return RedirectToAction("Appointment", "PriorAppointment");
                }

                else
                {
                    Session.ClearRequestIDSessionKey();
                    Session.SetSessionIndexSessionKey(sessionIndex);

                    // аутентифицировать юзера в системе
                    //ttpContext.Session.Clear(); // ?

                    ViewBag.MpassInfo = "Pentru Dvs. nu s-au gasit inregistrari in MPASS.";

                    return View("MpassInfo");
                }
            }

            throw new Exception("SamlResponse is missing. Method was called in wrong Logout order.");
        }

        [OutputCache(Duration = 0, Location = OutputCacheLocation.None, NoStore = true)]
        [HttpGet]
        public ActionResult MPASSLogout(string returnUrl)
        {
            // generate LogoutRequest ID
            var logoutRequestID = "_" + Guid.NewGuid();

            Session.SetRequestIDSessionKey(logoutRequestID);

            WriteLog.Common.Debug("Method MPASSLogout. LogoutRequestId: " + logoutRequestID);

            AppointmentModel appointment = ParseSamlUserData.ParseTo(new AppointmentModel());

            string _sessionIndexSessionKey = Session.GetSessionIndexSessionKey();

            WriteLog.Common.Debug("Method MPASSLogout. SessionIndexSessionKey: " + _sessionIndexSessionKey);

            var logoutRequest = SamlHelper.BuildLogoutRequest(logoutRequestID, MPASSConfiguration.SamlLogoutDestination, MPASSConfiguration.Issuer, appointment.IDNP, _sessionIndexSessionKey);

            // redirect to IdP
            ViewBag.IdPUrl = MPASSConfiguration.SamlLogoutDestination;
            ViewBag.SAMLVariable = "SAMLRequest";
            ViewBag.SAMLMessage = SamlHelper.Encode(logoutRequest);

            // NOTE: Optional (remove if not needed) RelayState may be maximum 80 bytes, as specified by [SAMLBind, 3.5.3]
            ViewBag.RelayState = "eFile LogoutRequest Relay State";

            return View("MPASSRedirect");

        }

        [HttpPost]
        public ActionResult MPASSLogoutResponse(string samlResponse, string relayState)
        {
            XmlNamespaceManager ns;//Неясная хрень, зато понятно в примере где находимся.
            bool statusResultText = false;

            var expectedUrl = string.Format("{0}/{2}/{1}", RequestBaseUrl(), "MPASSLogoutResponse", "Authentication");

            string _RequestIDSessionKey = Session.GetRequestIDSessionKey();

            WriteLog.Common.Debug("Method: MPASSLogoutResponse. ExpectedUrl: " + expectedUrl);

            WriteLog.Common.Debug("Method: MPASSLogoutResponse. _RequestIDSessionKey: " + _RequestIDSessionKey);

            SamlHelper.LoadAndVerifyLogoutResponse(samlResponse, expectedUrl, TimeSpan.Parse(MPASSConfiguration.SamlMessageTimeout), _RequestIDSessionKey, out ns, out statusResultText);

            Session.ClearLoginTypeKey();

            Session.ClearRequestIDSessionKey();

            Session.ClearSessionUser();

            FormsAuthentication.SignOut();

            return RedirectToAction("MpassAuthentication");
        }

        /// <summary>
        /// Single Logout
        /// </summary>
        /// <param name="samlRequest"></param>
        /// <param name="relayState"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MPASSLogoutRequest(string samlRequest, string relayState) // Single Logout
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //Подозреваю последовательность такая
                var postUrl = string.Format("{0}/{2}/{1}", RequestBaseUrl(), "MPASSLogoutRequest", "Authentication");

                AppointmentModel appointment = ParseSamlUserData.ParseTo(new AppointmentModel());

                string logoutRequestID;

                string _sessionIndexSessionKey = Session.GetSessionIndexSessionKey();

                SamlHelper.LoadAndVerifyLogoutRequest(samlRequest, postUrl, TimeSpan.Parse(MPASSConfiguration.SamlMessageTimeout), appointment.IDNP, _sessionIndexSessionKey, out logoutRequestID);

                //await SignOutAndCleanUser();

                // build LogoutResponse
                var logoutResponseID = "_" + Guid.NewGuid();
                var logoutResponse = SamlHelper.BuildLogoutResponse(logoutResponseID, MPASSConfiguration.SamlLogoutDestination, logoutRequestID, MPASSConfiguration.Issuer);

                // redirect to IdP
                ViewBag.IdPUrl = MPASSConfiguration.SamlLogoutDestination;

                ViewBag.SAMLVariable = "SAMLResponse";
                ViewBag.SAMLMessage = SamlHelper.Encode(logoutResponse);

                ViewBag.RelayState = relayState;
                return View("MPASSRedirect");
            }
            else
            {
                this.HttpContext.Response.StatusCode = 208;
                return Json(new { status = "Already Signed out" });
            }
        }

        //#######################    MPASS Methods    ###############################

        #endregion 





        [HttpPost]
        public ActionResult EndSession()
        {
            WriteLog.Common.Debug("Close page at: " + DateTime.Now);

            return Json(new { success = true });
        }

    }
}

