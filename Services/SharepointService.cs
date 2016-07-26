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
        private readonly ClientContext _clientContext;
        public SharepointService()
        {
            _clientContext = (ClientContext)System.Web.HttpContext.Current.Session["ClientContext"];
        }

        private List GetSharepointListByTitle(string nameOfList)
        {
            var web = _clientContext.Web;
            _clientContext.Load(web.Lists);
            _clientContext.ExecuteQuery();
            return _clientContext.Web.Lists.GetByTitle(nameOfList);
        }

        private ListItemCollection GetAllItems(List contextList)
        {
            var query = CamlQuery.CreateAllItemsQuery();
            var items = contextList.GetItems(query);
            _clientContext.Load(items);
            _clientContext.ExecuteQuery();
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
            if (_clientContext == null) return listOfCourses;

            var contextList = GetSharepointListByTitle("Courses");
            var items = GetAllItems(contextList);

            listOfCourses.AddRange(items.Select(listItem => new Course()
            {
                Title = listItem["Title"].ToString(),
                Id = Convert.ToInt32(listItem["qgjk"]),
                Description = listItem["b6yw"].ToString(),
                ImageUrl = listItem["_x0076_pe2"].ToString(),
                Path = listItem["i2ll"].ToString(),
                ShortDescription = listItem["_x0062_r91"].ToString(),
                Author = listItem["yyid"].ToString()

            }));
            return listOfCourses;
        }

        public List<Movie> GetAllMovies()
        {
            var listOfMovies = new List<Movie>();
            if (_clientContext == null) return listOfMovies;
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
            return listOfMovies;
        }

        public List<Movie> GetMoviesFromCourse(List<Movie> movies, int id)
        {
            return movies.Where(x => x.CourseId == id).ToList();
        }

        public List<Question> GetQuestions(int courseId)
        {
            var listOfQuestions = new List<Question>();
            if (_clientContext == null) return listOfQuestions;
            var contextList = GetSharepointListByTitle("Questions");
            var items = GetAllItems(contextList);

            foreach (var item in items)
            {
                if (Int32.Parse(item["wm0t"].ToString()) == courseId)
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
                        TestId = Int32.Parse(item["wm0t"].ToString()),
                        Id = item.Id
                    });
                }
            }

            return listOfQuestions;
        }

        public List<TestResult> GetTestsResults(int userId)
        {
            var listOfSolvedTests = new List<TestResult>();
            if (_clientContext == null) return listOfSolvedTests;
            var contextList = GetSharepointListByTitle("SolvedTests");
            var items = GetAllItems(contextList);
            foreach (var item in items)
            {
                listOfSolvedTests.Add(new TestResult()
                {
                    CourseName = item["Title"].ToString(),
                    UserId = Int32.Parse(item["elvy"].ToString()),
                    Result = (item["i4lh"].ToString()),
                    CourseId = Int32.Parse(item["f8dk"].ToString()),
                    DateOfTest = Convert.ToDateTime(item["Created"].ToString())
                });
            }
            return listOfSolvedTests.Where(x => x.UserId == userId).ToList();

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

        public Course GetCourse(int id)
        {
            var course = new Course();
            var contextList = GetSharepointListByTitle("Courses");
            var items = GetAllItems(contextList);

            foreach (var item in items.Where(item => Convert.ToInt32(item["qgjk"]) == id))
            {
                course.Title = item["Title"].ToString();
                course.Id = Convert.ToInt32(item["qgjk"]);
                course.Description = item["b6yw"].ToString();
                course.ImageUrl = item["_x0076_pe2"].ToString();
                course.Path = item["i2ll"].ToString();
                course.ShortDescription = item["_x0062_r91"].ToString();
                course.Author = item["yyid"].ToString();
            }
            return course;
        }

        public int GetUserId()
        {
            var spContext = ((SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"]);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.CurrentUser);
                    clientContext.ExecuteQuery();
                    return clientContext.Web.CurrentUser.Id;
                }
            }
            return 0;
        }

        public
            List<Post> GetDiscussionPosts(string courseId)
        {
            var listOfPosts = new List<Post>();
            if (_clientContext == null) return listOfPosts;
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
            var spContext = ((SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"]);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Discussion");
                    if (contextList == null) return true;
                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    ListItemCollection items = contextList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
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
                }
            }
            return true;
        }

        public bool AddResultOfTest(TestResult testResult)
        {
            var contextList = GetSharepointListByTitle("SolvedTests");
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = contextList.AddItem(itemCreateInfo);
            listItem["Title"] = testResult.CourseName;
            listItem["elvy"] = testResult.UserId;
            listItem["i4lh"] = testResult.Result;
            listItem["f8dk"] = testResult.CourseId;
            listItem.Update();
            _clientContext.ExecuteQuery();
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
                    _clientContext.ExecuteQuery();
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
                    _clientContext.ExecuteQuery();
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
                    _clientContext.ExecuteQuery();
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
            _clientContext.ExecuteQuery();

            return true;
        }

        public List<Course> CountMoviesInCourse(List<Course> courses, List<Movie> movies)
        {
            foreach (var item in movies)
            {
                var firstOrDefault = courses.FirstOrDefault(x => x.Id == item.CourseId);
                if (firstOrDefault != null)
                    firstOrDefault.NumberOfMovies++;
            }
            return courses;
        }
    }
}