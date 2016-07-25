using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class HomeController : Controller
    {

        [SharePointContextFilter]
        public ActionResult Index()
        {
            InitializeSession();
            return View();
        }


        private void InitializeSession()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            var clientContext = spContext.CreateAppOnlyClientContextForSPHost();
            Session.Add("SharepointContext", spContext);
            Session.Add("ClientContext", clientContext);
            clientContext = spContext.CreateUserClientContextForSPHost();
            Session.Add("logged", false);

        }

    }
}
