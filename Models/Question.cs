using System.Collections.Generic;

namespace E_LearningWeb.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public int TestId { get; set; }

        public Question()
        {
            Answers = new List<string>();
        }
    }
}