using E_LearningWeb.Models;
using E_LearningWeb.Repositories;
using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private readonly IUnitOfWork _unitOfWork;
        private List<Question> _questions;


        public TestController(ISharepointService sharepointService, IUnitOfWork unitOfWork)
        {
            _sharepointService = sharepointService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int courseId)
        {
            var testViewModel = new TestViewModel(_unitOfWork.Questions.Find(x => x.CourseId == courseId).ToList());

            return View(testViewModel);
        }

        public ActionResult Result(string correctAnswers, string incorrectAnswers, int courseId)
        {
            _unitOfWork.TestResults.AddTestResult(correctAnswers, courseId);
            _questions = _unitOfWork.Questions.GetQuestionsWithAnswers(courseId).ToList();

            return
                View(new TestViewModel(DataConversionService.GetIncorrectQuestions(incorrectAnswers, _questions),
                   correctAnswers));

        }

        public ActionResult ScoreBoard()
        {
            var users = _sharepointService.GetSharepointUsers();
            users = DataConversionService.GetCorrectAnswerForEachUser(users);
            return View(users.OrderByDescending(x => x.CorrectAnswers).ToList());
        }

        public ActionResult ResultHistory()
        {
            int userId = _sharepointService.GetUserId();
            var results = _unitOfWork.TestResults.Find(x => x.UserId == userId).ToList();
            return View(results);
        }

    }
}