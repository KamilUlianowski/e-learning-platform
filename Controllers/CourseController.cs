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
            return PartialView("Comments");
        }

        [HttpPost]
        public ActionResult AddPost(string text, string courseId)
        {
            _sharepointService.AddPost(text, courseId);
            var comments = _sharepointService.GetDiscussionPosts(Int32.Parse(courseId));

            var courseViewModel = new CourseViewModel()
            {
                ListOfPosts = comments,
                SpecificCourse = new Course()
                {
                    Id = Int32.Parse(courseId)
                }
            };
            return PartialView("Comments", courseViewModel);
        }

        [HttpPost]
        public void AddVote(int movieId, double rating)
        {
            _unitOfWork.Movies.AddVote(movieId, rating);
            _unitOfWork.Complete();
        }

        public ActionResult Search(string searchedText)
        {
            var movies = _unitOfWork.Movies.Find(x => x.Title.Contains(searchedText)).ToList();
            return View(movies);
        }

        [HttpPost]
        public ActionResult AddMovie(CourseViewModel courseViewModel, int courseId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ListOfCourses", "Course");
            var newMovie = new Movie()
            {
                CourseId = courseViewModel.NewMovie.CourseId,
                VideoUrl = "https://www.youtube.com/embed/" + DataConversionService.GetVideoId(courseViewModel.NewMovie.VideoUrl),
                Title = courseViewModel.NewMovie.Title
            };
            _unitOfWork.Movies.Add(newMovie);
            _unitOfWork.Complete();
            var newCourseViewModel = new CourseViewModel()
            {
                ListOfMovies = _unitOfWork.Movies.Find(x => x.CourseId == courseId).ToList()
            };
            return PartialView("Movies", newCourseViewModel);
        }

        public ActionResult DeleteMovie(int id, int courseId)
        {
            var movieToDelete = _unitOfWork.Movies.FirstOrDefault(x => x.Id == id);
            _unitOfWork.Movies.Remove(movieToDelete);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Course", new { courseId = courseId });
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


    }
}