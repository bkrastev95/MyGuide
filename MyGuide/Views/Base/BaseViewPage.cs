using MyGuide.Models;
using System.Web.Mvc;

namespace MyGuide.Views.Base
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new User User
        {
            get { return Session["USER"] as User; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new User User
        {
            get { return Session["USER"] as User; }
        }
    }
}