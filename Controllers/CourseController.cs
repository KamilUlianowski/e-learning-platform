using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using Microsoft.SharePoint.Client;
using System;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        public ActionResult Index(string courseId)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseId = Int32.Parse(courseId)
            };
            var spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];

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
                        if ((Convert.ToInt32(listItem["b9dk"]) != Convert.ToInt32(courseId))) continue;
                        courseViewModel.ListOfMovies.Add(new Movie()
                        {
                            CourseId = Convert.ToInt32(listItem["b9dk"]),
                            VideoUrl = listItem["meuv"].ToString(),
                            Title = listItem["Title"].ToString(),
                            Id = Convert.ToInt32(listItem["jkyq"]), // Nazwy kolumn w sharepoint

                        });

                    }
                }
            }
            return View(courseViewModel);
        }
    }
}