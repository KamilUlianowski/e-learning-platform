using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;
using static System.Int32;

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
            var listOfCourses = _sharepointService.GetCourses();
            return View(listOfCourses);
        }

        [HttpGet]
        public ActionResult Index(string courseId, string courseName)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseId = Parse(courseId),
                ListOfPosts = _sharepointService.GetDiscussionPosts(courseName),
                ListOfMovies = _sharepointService.GetMovies(Parse(courseId))
            };

            return View(courseViewModel);
        }

        public bool AddVote(int movieId, double rating)
        {
            return _sharepointService.AddVote(movieId, rating);
        }
    }
}