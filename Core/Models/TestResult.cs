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

        public TestResult()
        {
            DateOfTest = (DateTime.Now).AddHours(2);
        }

        public TestResult(string correctAnswers, int userId, int courseId, string courseName)
        {
            Result = correctAnswers;
            DateOfTest = (DateTime.Now).AddHours(2);
            CourseId = courseId;
            UserId = userId;
            CourseName = courseName;
        }
    }
}