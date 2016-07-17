using E_LearningWeb.Services;
using E_LearningWeb.ViewModels;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly ISharepointService _sharepointService;
        private static int _position;

        public TestController(ISharepointService sharepointService)
        {
            _sharepointService = sharepointService;
        }

        public ActionResult Index()
        {
            TestViewModel testViewModel = new TestViewModel();
            testViewModel.ListOfQuestions = _sharepointService.GetQuestions();
            return View(testViewModel);
        }

        public ActionResult Result()
        {
            return View();
        }
    }
}