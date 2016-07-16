using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public class TestService : ITestService
    {
        public List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            questions.Add(new Question()
            {
                Text = "What is the primary difference between a generic list and a generic dictionary? ",
                Answers = new List<string>()
                {
                    "List elements are accessed by positional index; dictionary elemenets are accessed by key.",
                    "List elements can by iterated; dictionary elements cannot be iterated.",
                    "List elements must be value types; dictionary elements can be any type",
                },
                CorrectAnswer = 1
            });
            questions.Add(new Question()
            {
                Text = "Which of the following is the correct syntax for initializing a generic dictionary with string values and integer keys? ",
                Answers = new List<string>()
                {
                    "var elements = new Dictionary <string, int>();",
                    "var elements = new Dictionary (string, int);",
                    "var elements = new Dictionary <string, int>;",
                },
                CorrectAnswer = 1
            });


            return questions;
        }
    }
}