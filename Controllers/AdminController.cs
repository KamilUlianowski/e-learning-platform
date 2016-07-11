using System;
using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session.Add("logged", true);
            //   return RedirectToAction("Index", "Home",
            //new { SPHostUrl = System.Web.HttpContext.Current.Session["SharepointContext"] });

            return View();
        }

        public ActionResult DeleteMovie(int id)
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
                    ListItemCollection listItems = contextList.GetItems(CamlQuery.CreateAllItemsQuery());
                    clientContext.Load(listItems);
                    clientContext.ExecuteQuery();

                    foreach (ListItem listItem in listItems)
                    {
                        if (Convert.ToInt32(listItem["jkyq"]) == id)
                        {
                            ListItem itemDelete = listItems.GetById(id);
                            itemDelete.DeleteObject();
                            clientContext.ExecuteQuery();
                            break;
                        }
                    }

                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
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
                new { SPHostUrl = System.Web.HttpContext.Current.Session["SharepointContext"] });
            //  return View();
        }
    }
}