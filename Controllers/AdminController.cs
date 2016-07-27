using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IAzureSqlService _azureSqlService;

        public AdminController(ISharepointService sharepointService, IAzureSqlService azureSqlService)
        {
            _sharepointService = sharepointService;
            _azureSqlService = azureSqlService;
        }

        public ActionResult DeleteMovie(int id, int courseId)
        {
            _azureSqlService.DeleteMovie(id);
            return RedirectToAction("Index", "Course", new { courseId = courseId });
        }

        [HttpPost]
        public ActionResult AddMovie(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ListOfCourses", "Course");
            _azureSqlService.AddMovie(courseViewModel.NewMovie);
            return RedirectToAction("Index", "Course", new { courseId = courseViewModel.NewMovie.CourseId });
        }

        [HttpGet]
        public ActionResult UpdateMovie(int id)
        {
            var movie = _azureSqlService.GetSingleMovie(id);
            var newMovieViewModel = new NewMovieViewModel()
            {
                Id = movie.Id,
                CourseId = movie.CourseId,
                Title = movie.Title,
                VideoUrl = movie.VideoUrl
            };
            return View(newMovieViewModel);
        }

        [HttpPost]
        public ActionResult UpdateMovie(NewMovieViewModel movie)
        {
            _azureSqlService.UpdateMovie(movie);
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }

        public ActionResult Index()
        {
            Session.Add("logged", _sharepointService.CheckIfTheUserHasPermissions());
            return View();
        }
    }
}
