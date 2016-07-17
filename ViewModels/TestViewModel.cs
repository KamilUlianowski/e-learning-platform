using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.ViewModels
{
    public class TestViewModel
    {
        public List<Question> ListOfQuestions { get; set; }
        public int iterator { get; set; }
        public string hehe { get; set; }
        public TestViewModel()
        {
            hehe = "WTF WITH THIS JAVASCRIPT";
            ListOfQuestions = new List<Question>();
        }
    }
}