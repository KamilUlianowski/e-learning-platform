using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace E_LearningWeb.Services
{
    public class SharepointService : ISharepointService
    {
        private SharePointContext spContext;
        private ClientContext clientContext;
        public SharepointService()
        {
            spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];
            clientContext = spContext.CreateUserClientContextForSPHost();
        }

        private List GetSharepointListByTitle(string nameOfList)
        {
            var web = clientContext.Web;
            clientContext.Load(web.Lists);
            clientContext.ExecuteQuery();
            return clientContext.Web.Lists.GetByTitle(nameOfList);
        }

        private ListItemCollection GetAllItems(List contextList)
        {
            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            ListItemCollection items = contextList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();
            return items;
        }

        private string GetVideoId(string link)
        {
            var youtubeMatch =
             new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
             .Match(link);
            return youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty;
        }

        public List<Course> GetCourses()
        {
            var listOfCourses = new List<Course>();
            if (clientContext == null) return listOfCourses;

            var contextList = GetSharepointListByTitle("Courses");
            var items = GetAllItems(contextList);

            listOfCourses.AddRange(items.Select(listItem => new Course()
            {
                Title = listItem["Title"].ToString(),
                Id = Convert.ToInt32(listItem["qgjk"]),
                Description = listItem["b6yw"].ToString(),
                ImageUrl = listItem["_x0076_pe2"].ToString()
            }));
            return listOfCourses;
        }

        public List<Movie> GetMovies(int courseId)
        {
            var listOfMovies = new List<Movie>();
            if (clientContext == null) return listOfMovies;
            var contextList = GetSharepointListByTitle("Movies");
            var items = GetAllItems(contextList);

            listOfMovies.AddRange(items.Select(listItem => new Movie()
            {
                CourseId = Convert.ToInt32(listItem["b9dk"]),
                VideoUrl = listItem["meuv"].ToString(),
                SumOfVotes = Double.Parse(listItem["envb"].ToString()),
                Title = listItem["Title"].ToString(),
                Id = Convert.ToInt32(listItem["jkyq"]),
                NumberOfVotes = Convert.ToInt32(listItem["snyt"].ToString())
            }));
            System.Web.HttpContext.Current.Session.Add("MaxMovieId", listOfMovies.Max(x => x.Id));
            return listOfMovies.Where(x => x.CourseId == Convert.ToInt32(courseId)).ToList();
        }

        public List<Question> GetQuestions()
        {
            var listOfQuestions = new List<Question>();
            if (clientContext == null) return listOfQuestions;
            var contextList = GetSharepointListByTitle("Questions");
            var items = GetAllItems(contextList);

            foreach (var item in items)
            {
                listOfQuestions.Add(new Question()
                {
                    Text = item["emrq"].ToString(),
                    Answers = new List<string>()
                        {
                            item["igug"].ToString(),
                            item["_x0066_nv6"].ToString(),
                            item["_x0065_es5"].ToString()
                        },
                    CorrectAnswer = Int32.Parse(item["t2vj"].ToString()),
                    TestId = Int32.Parse(item["wm0t"].ToString())
                });
            }

            return listOfQuestions;
        }

        public Movie GetMovieInfo(int id)
        {
            var movie = new Movie();
            var contextList = GetSharepointListByTitle("Movies");
            var items = GetAllItems(contextList);

            foreach (ListItem listItem in items)
            {
                if (Convert.ToInt32(listItem["jkyq"]) == id)
                {
                    movie.Title = listItem["Title"].ToString();
                    movie.VideoUrl = listItem["meuv"].ToString();
                    movie.CourseId = Convert.ToInt32(listItem["b9dk"].ToString());
                    return movie;
                }
            }
            return movie;
        }

        public List<Post> GetDiscussionPosts(string courseId)
        {
            var listOfPosts = new List<Post>();
            if (clientContext == null) return listOfPosts;
            var contextList = GetSharepointListByTitle("Discussion");
            var items = GetAllItems(contextList);
            int id = 0;
            foreach (var item in items)
            {
                if (item["CourseId"] != null)
                {
                    var course = item["CourseId"].ToString();
                    if (string.Equals(courseId, course))
                    {
                        id = Int32.Parse(item["ID"].ToString());
                    }
                }
                if (item["ParentItemID"] != null && Int32.Parse(item["ParentItemID"].ToString()) == id)
                {
                    var help = (FieldUserValue)item["Author"];
                    listOfPosts.Add(new Post
                    {
                        Body = item["Body"].ToString(),
                        Created = item["Created"].ToString(),
                        Author = help.LookupValue

                    });
                }
            }
            return listOfPosts;

        }

        public bool AddPost(string text, string courseId)
        {
            var contextList = GetSharepointListByTitle("Discussion");
            var items = GetAllItems(contextList);
            int id = 0;
            foreach (var item in items)
            {
                if (item["CourseId"] != null)
                {
                    var course = item["CourseId"].ToString();
                    if (string.Equals(course, courseId))
                    {
                        id = Int32.Parse(item["ID"].ToString());
                    }
                }
            }
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = contextList.AddItem(itemCreateInfo);
            listItem["Body"] = text;
            listItem["ParentItemID"] = id;
            listItem.Update();
            clientContext.ExecuteQuery();
            return true;

        }

        public bool AddVote(int movieId, double rating)
        {
            var contextList = GetSharepointListByTitle("Movies");
            var items = GetAllItems(contextList);

            foreach (ListItem listItem in items)
            {
                if (Convert.ToInt32(listItem["jkyq"]) == movieId)
                {
                    listItem["snyt"] = Int32.Parse(listItem["snyt"].ToString()) + 1; // Liczba głosów + 1
                    listItem["envb"] = Double.Parse(listItem["envb"].ToString()) + rating; // Dodanie głosu
                    listItem.Update();
                    clientContext.ExecuteQuery();
                    return true;
                }
            }
            return false;

        }

        public bool DeleteMovie(int id)
        {

            var contextList = GetSharepointListByTitle("Movies");
            var items = GetAllItems(contextList);

            foreach (ListItem listItem in items)
            {
                if (Convert.ToInt32(listItem["jkyq"]) == id)
                {
                    ListItem itemDelete = listItem;
                    itemDelete.DeleteObject();
                    clientContext.ExecuteQuery();
                    return true;
                }
            }
            return false;
        }

        public bool UpdateMovie(Movie movie)
        {
            var contextList = GetSharepointListByTitle("Movies");
            var items = GetAllItems(contextList);

            foreach (ListItem listItem in items)
            {
                if (Convert.ToInt32(listItem["jkyq"]) == movie.Id)
                {
                    listItem["Title"] = movie.Title;
                    listItem["meuv"] = movie.VideoUrl;
                    listItem["b9dk"] = movie.CourseId;
                    listItem.Update();
                    clientContext.ExecuteQuery();
                    return true;
                }
            }

            return false;
        }

        public bool AddMovie(Movie movie)
        {
            var contextList = GetSharepointListByTitle("Movies");
            string id = GetVideoId(movie.VideoUrl);
            string embedUrl =
            "https://www.youtube.com/embed/" + id;

            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = contextList.AddItem(itemCreateInfo);
            listItem["Title"] = movie.Title;
            listItem["meuv"] = embedUrl;
            listItem["b9dk"] = movie.CourseId;
            listItem["jkyq"] = Int32.Parse(System.Web.HttpContext.Current.Session["MaxMovieId"].ToString()) + 1;
            listItem["envb"] = 0;
            listItem["snyt"] = 0;

            listItem.Update();
            clientContext.ExecuteQuery();

            return true;
        }
    }
}