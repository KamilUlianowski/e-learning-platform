using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_LearningWeb.Services;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            var questions = _testService.GetQuestions();
            return View(questions);
        }
    }
}