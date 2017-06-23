using Resources;
using Resources.App_GlobalResources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyGuide.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetResources()
        {
            var set = Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            var dict = new Dictionary<string, string>();
            foreach(DictionaryEntry item in set)
            {
                dict[item.Key.ToString()] = item.Value.ToString();
            }

            return Json(dict, JsonRequestBehavior.AllowGet);
        }

        public void ChangeCulture(string lang)
        {
            Response.Cookies.Remove("Language");

            HttpCookie languageCookie = System.Web.HttpContext.Current.Request.Cookies["Language"];

            if (languageCookie == null) languageCookie = new HttpCookie("Language");

            languageCookie.Value = lang;

            languageCookie.Expires = DateTime.Now.AddDays(10);

            Response.SetCookie(languageCookie);

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult About()
        {
            ViewBag.Message = Resource.Home;

            return View();
        }
    }
}