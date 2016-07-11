using System.Web.Mvc;
using E_LearningWeb.Models;

namespace E_LearningWeb.Controllers
{
    public class VideoController : Controller
    {
        public ActionResult Index(string videoUrl)
        {
            var exampeMovie = new Movie()
            {
                VideoUrl = videoUrl
            };
            return View(exampeMovie);
        }
    }
}