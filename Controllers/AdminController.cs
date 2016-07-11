using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private static SharePointContext spContext;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddMovie()
        {
            if (spContext == null)
            {
                spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Movies");


                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Title"] = movie.Title;
                    listItem["meuv"] = movie.VideoUrl;
                    listItem["b9dk"] = movie.CourseId;
                    listItem["jkyq"] = 100;

                    listItem.Update();
                    clientContext.ExecuteQuery();
                }
            }
            return View();
        }
    }
}