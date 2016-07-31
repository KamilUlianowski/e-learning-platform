using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Linq;
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
            var movie = _unitOfWork.Movies.FirstOrDefault(x => x.Id == id);
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
            var movieToUpdate = _unitOfWork.Movies.FirstOrDefault(x => x.Id == movie.Id);
            movieToUpdate.CourseId = movie.CourseId;
            movieToUpdate.VideoUrl = movie.VideoUrl;
            movieToUpdate.Title = movie.Title;
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }

        public ActionResult Index()
        {
            Session.Add("logged", _sharepointService.CheckIfTheUserHasPermissions());
            return View();
        }
    }
}
