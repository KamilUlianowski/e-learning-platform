using E_LearningWeb.Models;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class HomeController : Controller
    {

        [SharePointContextFilter]
        public ActionResult Index()
        {
            InitializeSession();
            return RedirectToAction("ListOfCourses", "Course");
        }


        private void InitializeSession()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            var clientContext = spContext.CreateAppOnlyClientContextForSPHost();
            Session.Add("SharepointContext", spContext);
            Session.Add("ClientContext", clientContext);
            Session.Add("logged", false);

        }

    }
}
