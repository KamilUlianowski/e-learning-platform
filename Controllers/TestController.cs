using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureSqlService _azureSqlService;
        private List<Question> _questions;
        private static string _answers;

        public TestController(ISharepointService sharepointService, IUnitOfWork unitOfWork, IAzureSqlService azureSqlService)
        {
            _sharepointService = sharepointService;
            _unitOfWork = unitOfWork;
            _azureSqlService = azureSqlService;
        }

        public ActionResult Index(int courseId)
        {
            var testViewModel = new TestViewModel
            {
                ListOfQuestions = _unitOfWork.Questions.Find(x => x.CourseId == courseId).ToList()
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
                CourseName = _unitOfWork.Courses.FirstOrDefault(x => x.Id == courseId).Title,
                DateOfTest = (DateTime.Now).AddHours(2)
            };


            _unitOfWork.TestResults.Add(testResult);
            _questions = _unitOfWork.Questions.GetQuestionsWithAnswers(courseId).ToList();
            _unitOfWork.Complete();

            return View(new TestViewModel()
            {
                CorrectAnswers = correctAnswers,
                ListOfQuestions = DataConversionService.GetIncorrectQuestions(incorrectAnswers, _questions)
            });
        }

        public ActionResult ResultHistory()
        {
            int userId = _sharepointService.GetUserId();
            var results =
                _unitOfWork.TestResults.Find(x => x.UserId == userId).ToList();
            return View(results);
        }

        public void SaveAnswers(string answers)
        {
            _answers = answers;
        }

    }
}