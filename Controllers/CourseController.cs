﻿using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
using System.Linq;
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
            var listOfMovies = _sharepointService.GetAllMovies();
            listOfCourses = _sharepointService.CountMovies(listOfCourses, listOfMovies);
            return View(listOfCourses);
        }

        [HttpGet]
        public ActionResult Index(string courseId)
        {
            var allMovies = _sharepointService.GetAllMovies();
            Session.Add("MaxMovieId", allMovies.Max(x => x.Id) + 1);
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                CourseId = Parse(courseId),
                ListOfPosts = _sharepointService.GetDiscussionPosts(courseId),
                ListOfMovies = _sharepointService.GetMoviesFromCourse(allMovies, Int32.Parse(courseId))
            };
            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Index(string text, string courseId)
        {
            _sharepointService.AddPost(text, courseId);
            return RedirectToAction("Index", "Course", new { courseId = courseId });
        }

        public bool AddVote(int movieId, double rating)
        {
            return _sharepointService.AddVote(movieId, rating);
        }
    }
}