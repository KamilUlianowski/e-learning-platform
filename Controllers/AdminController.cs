using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private static string _spHostUrl = String.Empty;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddMovie()
        {
            if (_spHostUrl == null)
            {
                _spHostUrl = SharePointContext.GetSPHostUrl(System.Web.HttpContext.Current.Request).AbsoluteUri;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            var spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];
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
            return RedirectToAction("AddMovie", "Admin",
                new { SPHostUrl = _spHostUrl });
            //  return View();
        }
    }
}