using System.Web;
using static ProgramareAC.Services.MPASS.SamlHelper;

namespace ProgramareAC.Services.MPASS
{
    public static class SessionExtension
    {
        private const string SessionSamlUserKey = "SessionSamlUser";

        private const string RequestIDSessionKey = "SAML.RequestID";

        private const string SessionIndexSessionKey = "SAML.SessionIndex";

        private const string LoginTypeKey = "SessionLoginType";


        #region SamlUserData

        public static void SetSessionUser(this HttpSessionStateBase session, SAMLUserData user)
        {
            session[SessionSamlUserKey] = user;
        }

        public static SAMLUserData GetSessionUser(this HttpSessionStateBase session)
        {
            return session[SessionSamlUserKey] as SAMLUserData;
        }

        public static void ClearSessionUser(this HttpSessionStateBase session)
        {
            session.Remove(SessionSamlUserKey);
        }

        #endregion

        #region RequestIdSessionKey

        public static void SetRequestIDSessionKey(this HttpSessionStateBase session, string authnRequestID)
        {
            session[RequestIDSessionKey] = authnRequestID;
        }

        public static string GetRequestIDSessionKey(this HttpSessionStateBase session)
        {
            return session[RequestIDSessionKey] as string;
        }

        public static void ClearRequestIDSessionKey(this HttpSessionStateBase session)
        {
            session.Remove(RequestIDSessionKey);
        }

        #endregion

        #region SessionIndexSessionKey

        public static void SetSessionIndexSessionKey(this HttpSessionStateBase session, string sessionIndexSessionKey)
        {
            session[SessionIndexSessionKey] = sessionIndexSessionKey;
        }

        public static string GetSessionIndexSessionKey(this HttpSessionStateBase session)
        {
            return session[SessionIndexSessionKey] as string;
        }

        public static void ClearSessionIndexSessionKey(this HttpSessionStateBase session)
        {
            session.Remove(SessionIndexSessionKey);
        }

        #endregion

        #region LoginTypeKey

        public static void SetLoginTypeKey(this HttpSessionStateBase session, string loginTypeKey)
        {
            session[LoginTypeKey] = loginTypeKey;
        }

        public static string GetLoginTypeKey(this HttpSessionStateBase session)
        {
            return session[LoginTypeKey] as string;
        }

        public static void ClearLoginTypeKey(this HttpSessionStateBase session)
        {
            session.Remove(LoginTypeKey);
        }

        #endregion

    }
}