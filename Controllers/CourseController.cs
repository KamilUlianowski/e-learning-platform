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
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(ISharepointService sharepointService, IUnitOfWork unitOfWork)
        {
            _sharepointService = sharepointService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult ListOfCourses()
        {
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
                SpecificCourse = _unitOfWork.Courses.FirstOrDefault(x => x.Id == courseId),
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
        public void AddVote(int movieId, double rating)
        {
            _unitOfWork.Movies.AddVote(movieId, rating);
            _unitOfWork.Complete();
        }
    }
}