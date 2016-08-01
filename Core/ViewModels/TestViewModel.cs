using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.ViewModels
{
    public class TestViewModel
    {
        public List<Question> ListOfQuestions { get; set; }
        public string CorrectAnswers { get; set; }
        public TestViewModel()
        {
            ListOfQuestions = new List<Question>();
        }

        public TestViewModel(List<Question> questions)
        {
            ListOfQuestions = questions;
        }

        public TestViewModel(List<Question> questions, string correctAnswers)
        {
            ListOfQuestions = questions;
            CorrectAnswers = correctAnswers;
        }
    }
}