using E_LearningWeb.Models;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IAzureSqlService _azureSqlService;
        private List<Question> _questions;
        private static string _answers;

        public TestController(ISharepointService sharepointService, IAzureSqlService azureSqlService)
        {
            _sharepointService = sharepointService;
            _azureSqlService = azureSqlService;
        }

        public ActionResult Index(int courseId)
        {
            var testViewModel = new TestViewModel
            {
                ListOfQuestions = _azureSqlService.GetQuestions(courseId)
            };
            _questions = testViewModel.ListOfQuestions;
            return View(testViewModel);
        }

        public ActionResult Result(string correctAnswers, string incorrectAnswers, int courseId)
        {
            var testResult = new TestResult()
            {
                CourseId = courseId,
                Result = correctAnswers,
                UserId = _sharepointService.GetUserId(),
                CourseName = _azureSqlService.GetCourse(courseId).Title,
                DateOfTest = DateTime.Now
            };

            _azureSqlService.AddResultOfTest(testResult);
            _questions = _azureSqlService.GetQuestions(courseId);

            return View(new TestViewModel()
            {
                CorrectAnswers = correctAnswers,
                ListOfQuestions = DataConversionService.GetIncorrectQuestions(incorrectAnswers, _questions)
            });
        }

        public ActionResult ResultHistory()
        {
            var results = _azureSqlService.GetTestsResults(_sharepointService.GetUserId());
            return View(results);
        }

        public void SaveAnswers(string answers)
        {
            _answers = answers;
        }

    }
}