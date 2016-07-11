using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index(string courseId)
        {
            var listOfCourses = new List<Movie>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Movies");
                    
                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    ListItemCollection items = contextList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    foreach (ListItem listItem in items)
                    {
                        listOfCourses.Add(new Movie()
                        {
                            CourseId = Convert.ToInt32(listItem["b9dk"]),
                            VideoUrl = listItem["meuv"].ToString(),
                            Title = listItem["Title"].ToString(),
                            Id = Convert.ToInt32(listItem["jkyq"]), // Nazwy kolumn w sharepoint

                        });

                    }
                }
            }
            listOfCourses = listOfCourses.Where(x => x.CourseId == Int32.Parse(courseId)).ToList();
            return View(listOfCourses);
        }
    }
}