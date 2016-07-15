using E_LearningWeb.Models;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using Microsoft.SharePoint.Client;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISharepointService _sharepointService;

        public AdminController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        public ActionResult DeleteMovie(int id, int courseId)
        {
            _sharepointService.DeleteMovie(id);
            return RedirectToAction("Index", "Course", new { courseId = courseId });
        }

        [HttpGet]
        public ActionResult AddMovie(int id)
        {
            return View(new Movie() { CourseId = id });
        }

        [HttpPost]
        public ActionResult AddMovie(CourseViewModel courseViewModel)
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

                    string id = GetVideoId(courseViewModel.NewMovie.VideoUrl);
                    string embedUrl =
                    "https://www.youtube.com/embed/" + id;

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Title"] = courseViewModel.NewMovie.Title;
                    listItem["meuv"] = embedUrl;
                    listItem["b9dk"] = courseViewModel.CourseId;
                    listItem["jkyq"] = Int32.Parse(System.Web.HttpContext.Current.Session["MaxMovieId"].ToString()) + 1;
                    listItem["envb"] = 0;
                    listItem["snyt"] = 0;


                    listItem.Update();
                    clientContext.ExecuteQuery();
                }
            }
            return RedirectToAction("Index", "Course", new { courseId = courseViewModel.CourseId });
            //  return View();
        }

        [HttpGet]
        public ActionResult UpdateMovie(int id)
        {
            var movie = new Movie();
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
                            movie.Title = listItem["Title"].ToString();
                            movie.VideoUrl = listItem["meuv"].ToString();
                            movie.CourseId = Convert.ToInt32(listItem["b9dk"].ToString());
                            break;
                        }
                    }
                }

                return View(movie);
            }
        }

        [HttpPost]
        public ActionResult UpdateMovie(Movie movie)
        {
            _sharepointService.UpdateMovie(movie);
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }

        public string GetVideoId(string link)
        {
            var youtubeMatch =
             new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
             .Match(link);
            return youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty;
        }
    }
}
