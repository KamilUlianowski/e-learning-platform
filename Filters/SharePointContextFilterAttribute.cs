using System;
using System.Web;
using System.Web.Mvc;

namespace E_LearningWeb
{
    /// <summary>
    /// SharePoint action filter attribute.
    /// </summary>
    public class SharePointContextFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            Uri redirectUrl;
            switch (SharePointContextProvider.CheckRedirectionStatus(filterContext.HttpContext, out redirectUrl))
            {
                case RedirectionStatus.Ok:
                    return;
                case RedirectionStatus.ShouldRedirect:
                    filterContext.Result = new RedirectResult(redirectUrl.AbsoluteUri);
                    break;
                case RedirectionStatus.CanNotRedirect:
                    //filterContext.Result = new ViewResult { ViewName = "Error" };
                    filterContext.HttpContext = (HttpContextBase)System.Web.HttpContext.Current.Session["HttpContext"];
                    return;
            }
        }
    }
}
