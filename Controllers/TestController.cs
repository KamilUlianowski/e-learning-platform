using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private static int _position;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            Session.Add("Position", 1);
            TestViewModel testViewModel = new TestViewModel();
            testViewModel.ListOfQuestions = _testService.GetQuestions();
            return View(testViewModel);
        }

        public bool GetPosition()
        {
            Session["Position"] = ++_position;
            var z = Session["Position"];
            return true;
        }
    }
}