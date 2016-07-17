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
                CorrectAnswer = 0
            });
            questions.Add(new Question()
            {
                Text = "When creating an instance of an array using this code: var colorOptions = new string[4];  What is colorOptions ? ",

                Answers = new List<string>()
                {
                    "An array containing four elements, each of which is an empty string ",
                    "An array containing four elements, each of which is null ",
                    "An array containing no elements, but with room for four elements ",
                },
                CorrectAnswer = 1
            });
            questions.Add(new Question()
            {
                Text = "What are generics?",
                Answers = new List<string>()
                {
                    "A technique for returning multiple values from a method ",
                    "A technique for defining a data type using a variable ",
                    "A technique for defining interfaces ",
                },
                CorrectAnswer = 1
            });
            questions.Add(new Question()
            {
                Text = "What does this line of code do? colorOptions.Remove('White'); ",
                Answers = new List<string>()
                {
                    "Removes the first occurrance of the element 'White' from the list and leaves a null element ",
                    "Removes all occurrances of the element 'White' from the list and leaves null elements ",
                    "Removes the first occurrance of the element 'White' from the list and shifts up the elements below it ",
                },
                CorrectAnswer = 2
            });


            return questions;
        }
    }
}