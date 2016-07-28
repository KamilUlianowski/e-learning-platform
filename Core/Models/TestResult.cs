using System;

namespace E_LearningWeb.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string Result { get; set; }
        public DateTime DateOfTest { get; set; }
    }
}