using Microsoft.SharePoint.Client;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class HomeController : Controller
    {
        private static bool _sessionInitialized = false;

        [SharePointContextFilter]
        public ActionResult Index()
        {
            if (_sessionInitialized == false)
            {
                InitializeSession();
            }
            return View();
        }


        private void InitializeSession()
        {
            Session.Add("SharepointContext",
                SharePointContextProvider.Current.GetSharePointContext(HttpContext));
            var clientContext = ((SharePointContext)Session["SharepointContext"]).CreateAppOnlyClientContextForSPHost();
            Session.Add("logged", GetUserPermissions(clientContext));
            _sessionInitialized = true;

        }

        private bool GetUserPermissions(ClientContext clientContext)
        {
            BasePermissions bp = new BasePermissions();
            bp.Set(PermissionKind.ManageWeb);
            ClientResult<bool> manageWeb = clientContext.Web.DoesUserHavePermissions(bp);
            clientContext.ExecuteQuery();

            return manageWeb.Value;
        }

    }
}
