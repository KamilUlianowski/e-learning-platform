using E_LearningWeb.Models;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly SharepointService _sharepointService;

        public CourseController(SharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        [HttpGet]
        public ActionResult ListOfCourses()
        {
            var listOfCourses = new List<Course>();
            listOfCourses = _sharepointService.GetCourses();
            return View(listOfCourses);
        }

        [HttpPost]
        public ActionResult ListOfCourses(string text)
        {
            var spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Discussion");

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Body"] = text;

                    listItem.Update();
                    clientContext.ExecuteQuery();
                }
            }
            return RedirectToAction("Course", "Home");
        }

        [HttpGet]
        public ActionResult Index(string courseId, string courseName)
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
                        courseViewModel.ListOfMovies.Add(new Movie()
                        {
                            CourseId = Convert.ToInt32(listItem["b9dk"]),
                            VideoUrl = listItem["meuv"].ToString(),
                            SumOfVotes = Double.Parse(listItem["envb"].ToString()),
                            Title = listItem["Title"].ToString(),
                            Id = Convert.ToInt32(listItem["jkyq"]), // Nazwy kolumn w sharepoint
                            NumberOfVotes = Convert.ToInt32(listItem["snyt"].ToString())

                        });
                    }
                    System.Web.HttpContext.Current.Session.Add("MaxMovieId", courseViewModel.ListOfMovies.Max(x => x.Id));
                    courseViewModel.ListOfMovies =
                        courseViewModel.ListOfMovies.Where(x => x.CourseId == Convert.ToInt32(courseId)).ToList();

                    contextList = clientContext.Web.Lists.GetByTitle("Discussion");
                    items = contextList.GetItems(CamlQuery.CreateAllItemsQuery());

                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
                    int id = 0;
                    foreach (var item in items)
                    {
                        if (item["Title"] != null)
                        {
                            var title = item["Title"].ToString();
                            if (String.Equals(courseName, title))
                            {
                                id = Int32.Parse(item["ID"].ToString());
                            }
                        }
                        if (item["ParentItemID"] != null)
                        {
                            if (Int32.Parse(item["ParentItemID"].ToString()) == id)
                            {
                                FieldUserValue help = (FieldUserValue)item["Author"];
                                courseViewModel.ListOfPosts.Add(new Post
                                {
                                    Body = item["Body"].ToString(),
                                    Created = item["Created"].ToString(),
                                    Author = help.LookupValue

                                });
                            }
                        }
                    }
                }
            }
            return View(courseViewModel);
        }

        public bool AddVote(int movieId, double rating)
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
                        if (Convert.ToInt32(listItem["jkyq"]) == movieId)
                        {
                            listItem["snyt"] = Int32.Parse(listItem["snyt"].ToString()) + 1; // Liczba głosów + 1
                            listItem["envb"] = Double.Parse(listItem["envb"].ToString()) + rating; // Dodanie głosu
                            listItem.Update();
                            clientContext.ExecuteQuery();
                            break;
                        }
                    }

                }
                return true;
            }
        }
    }
}