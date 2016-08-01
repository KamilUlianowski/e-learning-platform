using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
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
            return View(new CourseViewModel(courseId));
        }

        [HttpPost]
        public ActionResult AddPost(string text, string courseId)
        {
            _sharepointService.AddPost(text, courseId);
            var comments = _sharepointService.GetDiscussionPosts(Int32.Parse(courseId));
            var course = new Course(int.Parse(courseId));
            var courseViewModel = new CourseViewModel(comments, course);

            return PartialView("Comments", courseViewModel);
        }

        [HttpPost]
        public void AddVote(int movieId, double rating)
        {
            _unitOfWork.Movies.AddVote(movieId, rating);
        }

        public ActionResult Search(string searchedText)
        {
            var courseViewModel =
                new CourseViewModel(_unitOfWork.Movies.Find(x => x.Title.Contains(searchedText)).ToList());

            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult AddMovie(CourseViewModel courseViewModel, int courseId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ListOfCourses", "Course");

            _unitOfWork.Movies.AddMovie(courseViewModel);

            var newCourseViewModel = new CourseViewModel(
                _unitOfWork.Movies.GetMoviesFromCourse(courseId).ToList());

            return PartialView("Movies", newCourseViewModel);
        }

        [HttpPost]
        public ActionResult DeleteMovie(int id, int courseId)
        {
            _unitOfWork.Movies.DeleteMovie(id);
            return PartialView("Movies", new CourseViewModel(_unitOfWork.Movies.GetMoviesFromCourse(courseId).ToList()));
        }

        [HttpPost]
        public ActionResult UpdateMovie(NewMovieViewModel movie)
        {
            _unitOfWork.Movies.UpdateMovie(movie);
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }


    }
}