using MyGuide.Domain.Services;
using MyGuide.Models;
using Resources.App_GlobalResources;
using System.Web.Mvc;
using System.Web.Security;

namespace MyGuide.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userService.Login(model);

                if (user != null)
                {
                    return HandleSuccessfulLogin(user, returnUrl);
                }
            }

            ModelState.AddModelError("", Resource.InvalidLogin);
            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home", null);

            // return new RedirectResult(Url.Action("Index", "Home", null));
        }

        private ActionResult HandleSuccessfulLogin(User user, string returnUrl)
        {
            Session[UserKey] = user;

            //FormsAuthentication.SetAuthCookie
            FormsAuthentication.SetAuthCookie(User.Username, false);
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = returnUrl.Trim('\'');

                var decoded = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(decoded))
                {
                    return Redirect(returnUrl);
                }                
            }
            
            return RedirectToAction("Index", "Home", null);
        }
    }
}