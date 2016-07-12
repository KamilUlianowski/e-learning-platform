using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> ListOfPosts { get; set; }
        public List<Course> ListOfCourses { get; set; }
        public string Text { get; set; }

        public HomeViewModel()
        {
            ListOfPosts = new List<Post>();
            ListOfCourses = new List<Course>();
        }
    }
}