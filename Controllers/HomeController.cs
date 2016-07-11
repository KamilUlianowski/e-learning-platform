using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using E_LearningWeb.Models;

namespace E_LearningWeb.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session.Add("SharepointContext",
                SharePointContextProvider.Current.GetSharePointContext(HttpContext));
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
