//base libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Configuration;
using System.Configuration;

//added imports

namespace SessionDemoApp
{
    public class Global : HttpApplication
    {
        private const string _WebApiPrefix = "api";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }


        private static string _WebApiExecutionPath = String.Format("~/{0}", _WebApiPrefix);

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private static bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(_WebApiExecutionPath);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            //get the useragent for the request
            string currentUserAgent = HttpContext.Current.Request.UserAgent;

            //decide if we need to strip off the same site attribute for older browsers
            bool dissallowSameSiteFlag = DisallowsSameSiteNone(currentUserAgent);

            //get the name of the cookie, if not defined default to the "ASP.NET_SessionID" value
            SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager
                                                        .GetSection("system.web/sessionState");
            string sessionCookieName;
            if (sessionStateSection != null)
            {
                //read the name from the configuration
                sessionCookieName = sessionStateSection.CookieName;
            }
            else
            {
                sessionCookieName = "ASP.NET_SessionId";
            }


            //should the flag be positioned to true, then remove the attribute by setting
            //value to SameSiteMode.None
            if (dissallowSameSiteFlag)
                Response.Cookies[sessionCookieName].SameSite = (SameSiteMode)(-1);

            //while we're at it lets also make it secure
            if (Request.IsSecureConnection)
                Response.Cookies[sessionCookieName].Secure = true;
        }


        private bool DisallowsSameSiteNone(string userAgent)
        {
            //check if user agent is null or empty
            if (!String.IsNullOrWhiteSpace(userAgent))
                return false;

            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS 
            // networking stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }

       

        void Session_End(object sender, EventArgs e)
        {
            //check if there is a variable inside called _redirectUrl and attempt to redirect there
            if (Session["_redirectUrl"] != null)
            {
                Response.Redirect(Session["_redirectUrl"].ToString());
            }

        }
    }
}