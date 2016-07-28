using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IAzureSqlService _azureSqlService;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(ISharepointService sharepointService, IAzureSqlService azureSqlService, IUnitOfWork unitOfWork)
        {
            _sharepointService = sharepointService;
            _azureSqlService = azureSqlService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult ListOfCourses()
        {
            // var listOfCourses = _azureSqlService.GetAllCourses().ToList();
            var listOfCourses = _unitOfWork.Courses.GetAll().ToList();
            var listOfMovies = _unitOfWork.Movies.GetAll().ToList();
            listOfCourses = DataConversionService.CountMoviesInCourse(listOfCourses, listOfMovies);
            return View(listOfCourses);
        }

        [HttpGet]
        public ActionResult Index(int courseId)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                SpecificCourse = _unitOfWork.Courses.SingleOrDefault(x => x.Id == courseId),
                ListOfQuestions = _unitOfWork.Questions.GetQuestionsWithAnswers(courseId).ToList(),
                ListOfMovies = _unitOfWork.Movies.Find(x => x.CourseId == courseId).ToList(),
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