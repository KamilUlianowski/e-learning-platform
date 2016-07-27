using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IAzureSqlService _azureSqlService;

        public CourseController(ISharepointService sharepointService, IAzureSqlService azureSqlService)
        {
            _sharepointService = sharepointService;
            _azureSqlService = azureSqlService;
        }

        [HttpGet]
        public ActionResult ListOfCourses()
        {
            var listOfCourses = _azureSqlService.GetAllCourses();
            var listOfMovies = _azureSqlService.GetAllMovies();
            listOfCourses = DataConversionService.CountMoviesInCourse(listOfCourses, listOfMovies);
            return View(listOfCourses);
        }

        [HttpGet]
        public ActionResult Index(string courseId)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                SpecificCourse = _azureSqlService.GetCourse(Int32.Parse(courseId)),
                ListOfQuestions = _azureSqlService.GetQuestions(Int32.Parse(courseId)),
                ListOfMovies = _azureSqlService.GetMoviesFromCourse(Int32.Parse(courseId)),
                ListOfPosts = _sharepointService.GetDiscussionPosts(courseId)
            };

            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Index(string text, string courseId)
        {
            _sharepointService.AddPost(text, courseId);
            return RedirectToAction("Index", "Course", new { courseId = courseId });
        }

        [HttpPost]
        public bool AddVote(int movieId, double rating)
        {
            return _azureSqlService.AddVote(movieId, rating);
        }
    }
}