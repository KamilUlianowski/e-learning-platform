using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningWeb.Models
{
    public class Answer
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }
    }
}