﻿using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult DeleteMovie(int id)
        {
            int course = 0;
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
                            course = Convert.ToInt32(listItem["b9dk"]);
                            ListItem itemDelete = listItem;
                            itemDelete.DeleteObject();
                            clientContext.ExecuteQuery();
                            break;
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Course", new { courseId = course });
        }

        [HttpGet]
        public ActionResult AddMovie(int id)
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

                    string id = GetVideoId(movie.VideoUrl);
                    string embedUrl =
                    "https://www.youtube.com/embed/" + id;

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Title"] = movie.Title;
                    listItem["meuv"] = embedUrl;
                    listItem["b9dk"] = movie.CourseId;
                    listItem["jkyq"] = 100;

                    listItem.Update();
                    clientContext.ExecuteQuery();
                }
            }
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
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
                        if (Convert.ToInt32(listItem["jkyq"]) == movie.Id)
                        {
                            listItem["Title"] = movie.Title;
                            listItem["meuv"] = movie.VideoUrl;
                            listItem["b9dk"] = movie.CourseId;
                            listItem.Update();
                            clientContext.ExecuteQuery();
                            break;
                        }
                    }

                }
            }
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
