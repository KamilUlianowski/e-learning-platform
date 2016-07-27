using System.Collections.Generic;

namespace E_LearningWeb.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public int SelectedAnswer { get; set; }
        public int TestId { get; set; }
        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}