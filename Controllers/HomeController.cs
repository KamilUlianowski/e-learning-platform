using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
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
                _sessionInitialized = true;
                System.Web.HttpContext.Current.Session.Add("logged", false);

                System.Web.HttpContext.Current.Session.Add("SharepointContext",
                    SharePointContextProvider.Current.GetSharePointContext(HttpContext));

                System.Web.HttpContext.Current.Session.Add("spHostUrl",
                    SharePointContext.GetSPHostUrl(System.Web.HttpContext.Current.Request).AbsoluteUri);
            }

            var listOfCourses = new List<Course>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Courses");

                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    ListItemCollection items = contextList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    foreach (ListItem listItem in items)
                    {
                        listOfCourses.Add(new Course()
                        {
                            Title = listItem["Title"].ToString(),
                            Id = Convert.ToInt32(listItem["qgjk"]), // Nazwy kolumn w sharepoint
                            Description = listItem["b6yw"].ToString(),
                            ImageUrl = listItem["_x0076_pe2"].ToString()
                        });


                    }
                }
            }

            return View(listOfCourses);
        }

    }
}
