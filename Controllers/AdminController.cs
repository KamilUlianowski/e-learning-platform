﻿using E_LearningWeb.Models;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISharepointService _sharepointService;

        public AdminController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        public ActionResult DeleteMovie(int id, int courseId)
        {
            _sharepointService.DeleteMovie(id);
            return RedirectToAction("Index", "Course", new { courseId = courseId });
        }

        [HttpGet]
        public ActionResult AddMovie(int id)
        {
            return View(new Movie() { CourseId = id });
        }

        [HttpPost]
        public ActionResult AddMovie(CourseViewModel courseViewModel)
        {
            courseViewModel.NewMovie.CourseId = courseViewModel.CourseId;
            _sharepointService.AddMovie(courseViewModel.NewMovie);
            return RedirectToAction("Index", "Course", new { courseId = courseViewModel.CourseId });
        }

        [HttpGet]
        public ActionResult UpdateMovie(int id)
        {
            return View(_sharepointService.GetMovieInfo(id));
        }

        [HttpPost]
        public ActionResult UpdateMovie(Movie movie)
        {
            _sharepointService.UpdateMovie(movie);
            return RedirectToAction("Index", "Course", new { courseId = movie.CourseId });
        }
    }
}
