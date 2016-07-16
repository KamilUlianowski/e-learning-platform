using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;
using static System.Int32;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ISharepointService _sharepointService;

        public CourseController(ISharepointService sharepointService)
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
        public ActionResult Index(string courseId)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseId = Parse(courseId),
                ListOfPosts = _sharepointService.GetDiscussionPosts(courseId),
                ListOfMovies = _sharepointService.GetMovies(Parse(courseId))
            };
            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Index(string text, string courseId)
        {
            _sharepointService.AddPost(text, courseId);
            return RedirectToAction("Index", "Course", new {courseId = courseId});
        }

        public bool AddVote(int movieId, double rating)
        {
            return _sharepointService.AddVote(movieId, rating);
        }
    }
}