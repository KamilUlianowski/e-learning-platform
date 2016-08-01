using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(ISharepointService sharepointService, IUnitOfWork unitOfWork)
        {
            _sharepointService = sharepointService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult UpdateMovie(int id)
        {
            var movie = _unitOfWork.Movies.GetSingleMovie(id);
            if (movie != null)
            {
                return View(new NewMovieViewModel(movie));
            }
            return RedirectToAction("ListOfCourses", "Course");
        }

        [HttpPost]
        public ActionResult UpdateMovie(NewMovieViewModel movie)
        {
            _unitOfWork.Movies.UpdateMovie(movie);
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }

        public ActionResult Index()
        {
            Session.Add("logged", _sharepointService.CheckIfTheUserHasPermissions());
            return View();
        }
    }
}
