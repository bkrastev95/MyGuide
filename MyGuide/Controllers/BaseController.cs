using MyGuide.Models;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyGuide.Controllers
{
    public class BaseController : Controller
    {
        protected const string UserKey = "USER";
        protected const string DefaultLanguageKey = "defaultlanguage";
        protected readonly string DefaultLanguage;

        public BaseController()
        {
            DefaultLanguage = ConfigurationManager.AppSettings["defaultlanguage"];
        }

        protected virtual new User User
        {
            get { return Session[UserKey] as User; }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            HttpCookie languageCookie = System.Web.HttpContext.Current.Request.Cookies["Language"];
            if (languageCookie != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCookie.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(DefaultLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(DefaultLanguage);
            }

            base.Initialize(requestContext);
        }
    }
}